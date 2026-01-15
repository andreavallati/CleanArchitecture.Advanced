using AutoMapper;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Repositories;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Services;
using CleanArchitecture.Advanced.Api.Domain.Entities;
using CleanArchitecture.Advanced.Common.Application.DTOs;
using CleanArchitecture.Advanced.Common.Application.Requests;
using CleanArchitecture.Advanced.Common.Exceptions;
using FluentValidation;
using System.Linq.Expressions;

namespace CleanArchitecture.Advanced.Api.Application.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Library> _validator;
        private readonly INotificationService _notificationService;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IEventLogRepository _eventLogRepository;

        public LibraryService(IMapper mapper,
                              IValidator<Library> validator,
                              INotificationService notificationService,
                              ILibraryRepository libraryRepository,
                              IEventLogRepository eventLogRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _libraryRepository = libraryRepository ?? throw new ArgumentNullException(nameof(libraryRepository));
            _eventLogRepository = eventLogRepository ?? throw new ArgumentNullException(nameof(eventLogRepository));
        }

        public async Task<IEnumerable<LibraryDTO>> GetAllLibrariesAsync()
        {
            var entities = await _libraryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LibraryDTO>>(entities);
        }

        public async Task<IEnumerable<LibraryDTO>> GetFilteredLibrariesAsync(SearchRequest filterRequest)
        {
            var entities = await _libraryRepository.GetFilteredLibrariesAsync(filterRequest);
            return _mapper.Map<IEnumerable<LibraryDTO>>(entities);
        }

        public async Task<IEnumerable<LibraryDTO>> GetWhereLibrariesAsync(GetWhereRequest getWhereRequest)
        {
            // Build GetWhere expression
            Expression<Func<Library, bool>> expression = library =>
                                                         library.Name == getWhereRequest.Name &&
                                                         library.Address == getWhereRequest.Address;

            var entities = await _libraryRepository.GetWhereAsync(expression);
            return _mapper.Map<IEnumerable<LibraryDTO>>(entities);
        }

        public async Task<LibraryDTO> GetLibraryByIdAsync(long libraryId)
        {
            try
            {
                var entity = await _libraryRepository.GetByIdAsync(libraryId);
                return _mapper.Map<LibraryDTO>(entity);
            }
            catch (InvalidOperationException)
            {
                // Return null when entity is not found (for 404 handling in controller)
                return null!;
            }
        }

        public async Task<LibraryDTO> FirstLibraryAsync()
        {
            try
            {
                var entity = await _libraryRepository.FirstEntityAsync();
                return _mapper.Map<LibraryDTO>(entity);
            }
            catch (InvalidOperationException)
            {
                // Return null when entity is not found (for 404 handling in controller)
                return null!;
            }
        }

        public async Task<LibraryDTO> FirstLibraryAsync(FirstEntityRequest firstEntityRequest)
        {
            try
            {
                // Build FirstEntity expression
                Expression<Func<Library, bool>> expression = library =>
                                                             library.Name == firstEntityRequest.Name &&
                                                             library.Address == firstEntityRequest.Address;

                var entity = await _libraryRepository.FirstEntityAsync(expression);
                return _mapper.Map<LibraryDTO>(entity);
            }
            catch (InvalidOperationException)
            {
                // Return null when entity is not found (for 404 handling in controller)
                return null!;
            }
        }

        public async Task<IEnumerable<string>> SelectLibrariesNamesAsync()
        {
            // Build Select expression
            Expression<Func<Library, string>> expression = library => library.Name ?? default!;

            return await _libraryRepository.SelectAsync(expression);
        }

        public async Task<bool> InsertLibraryAsync(LibraryDTO library)
        {
            var entity = _mapper.Map<Library>(library);

            var validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await _libraryRepository.InsertAsync(entity);

            var eventLog = new EventLog
            {
                EventType = "Library-Insert",
                Description = $"Library {library.Name} has been properly added.",
            };

            await _eventLogRepository.InsertAsync(eventLog);

            // Call CommitChangesAsync on one of the repositories to commit both changes in one transaction
            await _libraryRepository.CommitChangesAsync();

            // Use external service for notification
            await _notificationService.SendNotificationAsync($"Library {library.Name} has been properly added.");

            // Insert properly executed
            return true;
        }

        public async Task<bool> UpdateLibraryAsync(LibraryDTO library)
        {
            var entity = _mapper.Map<Library>(library);

            var validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await _libraryRepository.UpdateAsync(entity);

            var eventLog = new EventLog
            {
                EventType = "Library-Update",
                Description = $"Library {library.Name} has been properly updated.",
            };

            await _eventLogRepository.InsertAsync(eventLog);

            // Call CommitChangesAsync on one of the repositories to commit both changes in one transaction
            await _libraryRepository.CommitChangesAsync();

            // Use external service for notification
            await _notificationService.SendNotificationAsync($"Library {library.Name} has been properly updated.");

            // Update properly executed
            return true;
        }

        public async Task<bool> DeleteLibraryAsync(long libraryId)
        {
            try
            {
                var library = await _libraryRepository.GetByIdAsync(libraryId);

                await _libraryRepository.DeleteAsync(libraryId);

                var eventLog = new EventLog
                {
                    EventType = "Library-Delete",
                    Description = $"Library {library.Name} has been properly deleted.",
                };

                await _eventLogRepository.InsertAsync(eventLog);

                // Call CommitChangesAsync on one of the repositories to commit both changes in one transaction
                await _libraryRepository.CommitChangesAsync();

                // Use external service for notification
                await _notificationService.SendNotificationAsync($"Library {library.Name} has been properly deleted.");

                // Delete properly executed
                return true;
            }
            catch (InvalidOperationException ex)
            {
                // Re-throw with more context when entity not found
                throw new InvalidOperationException($"Library with ID {libraryId} not found.", ex);
            }
        }
    }
}
