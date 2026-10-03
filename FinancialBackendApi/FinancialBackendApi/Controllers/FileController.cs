using FinancialBackendApi.Data;
using FinancialBackendApi.Models.DTOs;
using FinancialBackendApi.Models.Entities;
using FinancialBackendApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// The logger instance for logging information, warnings, and errors.
        /// </summary>
        private readonly ILogger<FileController> _logger;

        /// <summary>
        /// The financial database context for accessing file headers and details.
        /// </summary>
        private readonly FinancialDbContext _context;

        /// <summary>
        /// The file service for handling file-related operations.
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The financial database context.</param>
        /// <param name="fileService">The file service.</param>
        public FileController(ILogger<FileController> logger, FinancialDbContext context, IFileService fileService)
        {
            _logger = logger;
            _context = context;
            _fileService = fileService;
        }

        /// <summary>
        /// Gets a list of available files.
        /// </summary>
        /// <returns>A list of file names.</returns>
        [HttpGet(Name = "GetFiles")]
        public async Task<ActionResult<IEnumerable<FileHeaderDto>>> Get(int page = 1, int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize must be greater than 0.");
            }

            var files = await _fileService.GetFilesAsync(page, pageSize);

            return Ok(files);
        }


        /// <summary>
        /// Async method to return a specific file by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was retrieved.
        /// </returns>
        [HttpGet("{id}", Name = "GetFile")]
        public async Task<ActionResult<FileWithDetailsDto>> GetFile(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid File ID: {id}");
            }

            var file = await _fileService.GetFileWithDetailsAsync(id);

            return file is not null ? Ok(file) : NotFound(("File Not Found: Fileid", id));
        }


        /// <summary>
        /// Gets the detail object by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A list of file details.
        /// </returns>
        [HttpGet("{id}/details", Name = "GetFileDetails")]
        public async Task<ActionResult<IEnumerable<FileDetailDto>>> GetFileDetails(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid File ID: {id}");
            }

            var file = await _fileService.GetFileDetailDtosAsync(id);

            return file is not null ? Ok(file) : NotFound(("File Not Found: Fileid", id));

        }


        /// <summary>
        /// Deletes a specific file by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was deleted.
        /// </returns>
        [HttpDelete("{id}", Name = "DeleteFile")]
        public async Task<ActionResult> DeleteFile(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid File ID: {id}");
            }

            var file = await _fileService.DeleteFileAsync(id);

            if (file)
                return NoContent();

            return NotFound(("Not Found: FileId", id));
        }


        /// <summary>
        /// TODO - finish this later after hooking up with the database.
        /// </summary>
        /// <returns>
        /// A success message indicating the file was uploaded.
        /// </returns>
        [HttpPost(Name = "UploadFile")]
        public ActionResult<string> UploadFile(IFormFile file)
        {

            var nextId = _context.FileHeaders.Max(x => x.Id) + 1;

            _context.FileHeaders.Add(new FileHeader()
            {
                Id = nextId,
                FileName = file.FileName,
                Created = DateTime.UtcNow,
                Status = "Pending"
            });

            return CreatedAtAction("GetFile", new { id = nextId }, file);
        }


        /// <summary>
        /// Updates a specific file by its ID. 
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was updated.
        /// </returns>
        [HttpPut("{id}", Name = "UpdateFile")]
        public async Task<ActionResult<string>> UpdateFile(string fileName)
        {
            bool doesFileExist = await _context.FileHeaders.FirstOrDefaultAsync(x => x.FileName == fileName) != null;
            if (doesFileExist)
                return NotFound($"File already exists: {fileName}");

            var file = new FileHeader
            {
                FileName = fileName,
                Updated = DateTime.UtcNow
                //Status = "Pending"
            };

            _context.FileHeaders.Add(file);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFile", new { id = file.Id }, file);
        }

    }
}
