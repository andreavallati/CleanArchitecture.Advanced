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
        public async Task<ActionResult<IEnumerable<LibraryDTO>>> GetAllLibrariesAsync()
        {
            var libraries = await _libraryService.GetAllLibrariesAsync();
            return Ok(libraries);
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<LibraryDTO>>> GetFilteredLibrariesAsync([FromBody] SearchRequest filterRequest)
        {
            var libraries = await _libraryService.GetFilteredLibrariesAsync(filterRequest);
            return Ok(libraries);
        }

        [HttpPost("get-where")]
        public async Task<ActionResult<IEnumerable<LibraryDTO>>> GetWhereLibrariesAsync([FromBody] GetWhereRequest getWhereRequest)
        {
            var libraries = await _libraryService.GetWhereLibrariesAsync(getWhereRequest);
            return Ok(libraries);
        }

        [HttpGet("{libraryId:long}")]
        public async Task<ActionResult<LibraryDTO>> GetLibraryByIdAsync(long libraryId)
        {
            var library = await _libraryService.GetLibraryByIdAsync(libraryId);
            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }

        [HttpGet("first")]
        public async Task<ActionResult<LibraryDTO>> FirstLibraryAsync()
        {
            var library = await _libraryService.FirstLibraryAsync();
            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }

        [HttpPost("first")]
        public async Task<ActionResult<LibraryDTO>> FirstLibraryAsync([FromBody] FirstEntityRequest firstEntityRequest)
        {
            var library = await _libraryService.FirstLibraryAsync(firstEntityRequest);
            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }

        [HttpGet("select")]
        public async Task<ActionResult<IEnumerable<string>>> SelectLibrariesNamesAsync()
        {
            var librariesNames = await _libraryService.SelectLibrariesNamesAsync();
            return Ok(librariesNames);
        }

        [HttpPost]
        public async Task<ActionResult<LibraryDTO>> InsertLibraryAsync([FromBody] LibraryDTO library)
        {
            await _libraryService.InsertLibraryAsync(library);
            // Return 201 Created with location header pointing to the new resource
            return CreatedAtAction(nameof(GetLibraryByIdAsync), new { libraryId = library.Id }, library);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLibraryAsync([FromBody] LibraryDTO library)
        {
            await _libraryService.UpdateLibraryAsync(library);
            // Return 204 No Content for successful updates (REST convention)
            return NoContent();
        }

        [HttpDelete("{libraryId:long}")]
        public async Task<ActionResult> DeleteLibraryAsync(long libraryId)
        {
            await _libraryService.DeleteLibraryAsync(libraryId);
            // Return 204 No Content for successful deletions (REST convention)
            return NoContent();
        }
    }
}
