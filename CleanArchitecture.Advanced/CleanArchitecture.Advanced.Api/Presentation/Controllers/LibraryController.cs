using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Advanced.Common.Application.DTOs;
using CleanArchitecture.Advanced.Common.Application.Requests;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Services;

namespace CleanArchitecture.Advanced.Api.Presentation.Controllers
{
    [ApiController]
    [Route("libraries")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService ?? throw new ArgumentNullException(nameof(libraryService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLibrariesAsync()
        {
            var libraries = await _libraryService.GetAllLibrariesAsync();
            return Ok(libraries);
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetFilteredLibrariesAsync([FromBody] SearchRequest filterRequest)
        {
            var libraries = await _libraryService.GetFilteredLibrariesAsync(filterRequest);
            return Ok(libraries);
        }

        [HttpPost("get-where")]
        public async Task<IActionResult> GetWhereLibrariesAsync([FromBody] GetWhereRequest getWhereRequest)
        {
            var libraries = await _libraryService.GetWhereLibrariesAsync(getWhereRequest);
            return Ok(libraries);
        }

        [HttpGet("{libraryId:long}")]
        public async Task<IActionResult> GetLibraryByIdAsync(long libraryId)
        {
            var library = await _libraryService.GetLibraryByIdAsync(libraryId);
            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }

        [HttpGet("first")]
        public async Task<IActionResult> FirstLibraryAsync()
        {
            var library = await _libraryService.FirstLibraryAsync();
            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }

        [HttpPost("first")]
        public async Task<IActionResult> FirstLibraryAsync([FromBody] FirstEntityRequest firstEntityRequest)
        {
            var library = await _libraryService.FirstLibraryAsync(firstEntityRequest);
            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }

        [HttpGet("select")]
        public async Task<IActionResult> SelectLibrariesNamesAsync()
        {
            var librariesNames = await _libraryService.SelectLibrariesNamesAsync();
            return Ok(librariesNames);
        }

        [HttpPost]
        public async Task<IActionResult> InsertLibraryAsync([FromBody] LibraryDTO library)
        {
            var success = await _libraryService.InsertLibraryAsync(library);
            return Ok(success);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLibraryAsync([FromBody] LibraryDTO library)
        {
            var success = await _libraryService.UpdateLibraryAsync(library);
            return Ok(success);
        }

        [HttpDelete("{libraryId:long}")]
        public async Task<IActionResult> DeleteLibraryAsync(long libraryId)
        {
            var success = await _libraryService.DeleteLibraryAsync(libraryId);
            return Ok(success);
        }
    }
}
