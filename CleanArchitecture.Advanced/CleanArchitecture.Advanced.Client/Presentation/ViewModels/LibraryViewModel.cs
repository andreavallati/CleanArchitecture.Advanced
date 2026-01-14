using FluentValidation;
using System.Text;
using System.Windows.Input;
using CleanArchitecture.Advanced.Client.Application.Interfaces.Services.Factory;
using CleanArchitecture.Advanced.Client.Application.Interfaces.Services;
using CleanArchitecture.Advanced.Client.Domain.Models;
using CleanArchitecture.Advanced.Client.Presentation.Interfaces;
using CleanArchitecture.Advanced.Client.Presentation.ViewModels.Base;
using CleanArchitecture.Advanced.Common.Application.Requests;

namespace CleanArchitecture.Advanced.Client.Presentation.ViewModels
{
    public class LibraryViewModel : ViewModelBase<LibraryModel>, ILibraryViewModel
    {
        private readonly ILibraryUIService _libraryUIService;
        private readonly StringBuilder _stringBuilder;

        private IEnumerable<LibraryModel> _libraries;
        private LibraryModel _selectedLibrary;
        private string _details;
        private string _name;
        private string _address;

        public LibraryViewModel(IFactoryUIService uiService, IValidator<LibraryModel> validator) : base(uiService, validator)
        {
            _libraryUIService = _uiService.CreateUIService<ILibraryUIService>();

            _stringBuilder = new StringBuilder(Details);

            LoadAllLibrariesCommand = new DelegateCommand(async () => await LoadAllLibraries());
            LoadFilteredLibrariesCommand = new DelegateCommand(async () => await LoadFilteredLibraries());
            PrintDetailsCommand = new DelegateCommand(async () => await PrintDetails(), () => SelectedLibrary is not null)
                .ObservesProperty(() => SelectedLibrary);
            InsertLibraryCommand = new DelegateCommand(async () => await InsertLibrary());
            UpdateLibraryCommand = new DelegateCommand(async () => await UpdateLibrary(), () => SelectedLibrary is not null)
                .ObservesProperty(() => SelectedLibrary);
            DeleteLibraryCommand = new DelegateCommand(async () => await DeleteLibrary(), () => SelectedLibrary is not null)
                .ObservesProperty(() => SelectedLibrary);
        }

        public ICommand LoadAllLibrariesCommand { get; }
        public ICommand LoadFilteredLibrariesCommand { get; }
        public ICommand PrintDetailsCommand { get; }
        public ICommand InsertLibraryCommand { get; }
        public ICommand UpdateLibraryCommand { get; }
        public ICommand DeleteLibraryCommand { get; }

        public IEnumerable<LibraryModel> Libraries
        {
            get => _libraries;
            set => SetProperty(ref _libraries, value);
        }

        public LibraryModel SelectedLibrary
        {
            get => _selectedLibrary;
            set => SetProperty(ref _selectedLibrary, value);
        }

        public string Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private async Task LoadAllLibraries()
        {
            var apiResponse = await _libraryUIService.GetAllLibrariesAsync();
            Libraries = apiResponse.IsSuccess ? apiResponse.Items : [];
        }

        private async Task LoadFilteredLibraries()
        {
            var filterRequest = new SearchRequest
            {
                Name = "Central Library",
                Address = "123 Main St"
            };

            var apiResponse = await _libraryUIService.GetFilteredLibrariesAsync(filterRequest);
            Libraries = apiResponse.IsSuccess ? apiResponse.Items : [];
        }

        private async Task PrintDetails()
        {
            var getByIdApiRespone = await _libraryUIService.GetLibraryByIdAsync(SelectedLibrary.Id);
            SelectedLibrary = getByIdApiRespone.IsSuccess ? getByIdApiRespone.Item : default!;

            Print($"Selected Library: {SelectedLibrary.Name} - {SelectedLibrary.Address}");

            var getWhereRequest = new GetWhereRequest
            {
                Name = SelectedLibrary.Name,
                Address = SelectedLibrary.Address
            };

            var getWhereApiResponse = await _libraryUIService.GetWhereLibrariesAsync(getWhereRequest);
            foreach (var library in getWhereApiResponse.Items)
            {
                Print($"GetWhere Libraries: {library.Name} - {library.Address}");
            }

            var firstEntityApiResponse = await _libraryUIService.FirstLibraryAsync();
            var firstEntity = firstEntityApiResponse.Item;

            Print($"FirstEntity: {firstEntity.Name} - {firstEntity.Address}");

            var firstEntityRequest = new FirstEntityRequest
            {
                Name = SelectedLibrary.Name,
                Address = SelectedLibrary.Address
            };

            var firstEntityFilteredApiResponse = await _libraryUIService.FirstLibraryAsync(firstEntityRequest);
            var firstEntityFiltered = firstEntityFilteredApiResponse.Item;

            Print($"FirstEntityFiltered: {firstEntityFiltered.Name} - {firstEntityFiltered.Address}");

            var selectApiResponse = await _libraryUIService.SelectLibrariesNamesAsync();
            foreach (var libraryField in selectApiResponse.Items)
            {
                Print($"SelectField Library: {libraryField}");
            }
        }

        private async Task InsertLibrary()
        {
            var newLibrary = new LibraryModel
            {
                Name = Name,
                Address = Address,
                Books =
                [
                    new BookModel
                    {
                        Title = "New Book 1",
                        PublishedYear = 2020,
                        Author = new AuthorModel { FirstName = "John", LastName = "Doe" },
                        Loans = []
                    },
                    new BookModel
                    {
                        Title = "New Book 2",
                        PublishedYear = 2021,
                        Author = new AuthorModel { FirstName = "Jane", LastName = "Doe" },
                        Loans = []
                    }
                ]
            };

            var validationResult = await _validator.ValidateAsync(newLibrary);
            if (!validationResult.IsValid)
            {
                // Handle validation errors
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                Print($"Validation failed: {errors}");
                return;
            }

            var apiResponse = await _libraryUIService.InsertLibraryAsync(newLibrary);

            if (!apiResponse.IsSuccess)
            {
                // Handle error
                Print($"Insert failed: {apiResponse.ErrorMessage}");
                return;
            }

            Print($"Inserted new library: {newLibrary.Name} - {newLibrary.Address}");

            await LoadAllLibraries();
        }

        private async Task UpdateLibrary()
        {
            var library = SelectedLibrary;
            library.Name = Name;
            library.Address = Address;

            var validationResult = await _validator.ValidateAsync(library);
            if (!validationResult.IsValid)
            {
                // Handle validation errors
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                Print($"Validation failed: {errors}");
                return;
            }

            var apiResponse = await _libraryUIService.UpdateLibraryAsync(library);

            if (!apiResponse.IsSuccess)
            {
                // Handle error
                Print($"Update failed: {apiResponse.ErrorMessage}");
                return;
            }

            Print($"Updated library: {SelectedLibrary.Name} - {SelectedLibrary.Address}");

            await LoadAllLibraries();
        }

        private async Task DeleteLibrary()
        {
            var apiResponse = await _libraryUIService.DeleteLibraryAsync(SelectedLibrary.Id);

            if (!apiResponse.IsSuccess)
            {
                // Handle error
                Print($"Delete failed: {apiResponse.ErrorMessage}");
                return;
            }

            Print($"Deleted library: {SelectedLibrary.Name} - {SelectedLibrary.Address}");

            await LoadAllLibraries();
        }

        private void Print(string line)
        {
            _stringBuilder.AppendLine(line);
            _stringBuilder.AppendLine();
            Details = _stringBuilder.ToString();
        }
    }
}
