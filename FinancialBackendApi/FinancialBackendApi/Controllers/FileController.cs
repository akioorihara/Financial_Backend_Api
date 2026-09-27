using FinancialBackendApi.Data;
using FinancialBackendApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly ILogger<FileController> _logger;
        private readonly FinancialDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The financial database context.</param>
        public FileController(ILogger<FileController> logger, FinancialDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Gets a list of available files.
        /// </summary>
        /// <returns>A list of file names.</returns>
        [HttpGet(Name = "GetFiles")]
        public ActionResult<IEnumerable<FileHeader>> Get(int page = 1, int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize must be greater than 0.");
            }

            return _context.FileHeaders
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
        }


        /// <summary>
        /// Gets a specific file by its ID.
        /// TODO - implement this method with DTO and database access.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was retrieved.
        /// </returns>
        [HttpGet("{id}", Name = "GetFile")]
        public ActionResult<string> GetFile(int id)
        {
            var file = _context.FileHeaders.FirstOrDefault(x => x.Id == id);
            var fileDetails = _context.FileDetails.Where(x => x.FileHeaderId == id);

            if (file != null)
            {
                return Ok(new
                {
                    FileHeader = file,
                    fileDetails = fileDetails
                });
            }

            return NotFound($"File not found; {id}");
        }


        /// <summary>
        /// Gets the details of a specific file by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A list of file details.
        /// </returns>
        [HttpGet("{id}/details", Name = "GetFileDetails")]
        public ActionResult<IEnumerable<FileDetail>> GetFileDetails(int id)
        {
            var file = _context.FileDetails.FirstOrDefault(x => x.Id == id);
            if (file == null)
                return NotFound($"File not found: {id}");

            return _context.FileDetails
                .Where(x => x.FileHeaderId == id)
                .OrderByDescending(x => x.Id)
                .Take(100).ToList();
        }


        /// <summary>
        /// Deletes a specific file by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was deleted.
        /// </returns>
        [HttpDelete("{id}", Name = "DeleteFile")]
        public ActionResult DeleteFile(int id)
        {

            var file = _context.FileHeaders.FirstOrDefault(x => x.Id == id);

            if (file == null)
                return NotFound($"File not found: {id}");

            // remove the file from the static list (simulating deletion)
            _context.FileHeaders.Remove(file);

            // remove the file details from the static list (simulating deletion)
            _context.FileDetails.RemoveRange(
                _context.FileDetails.Where(x => x.FileHeaderId == id));

            return NoContent();
        }


        /// <summary>
        /// TODO - finish this later after hooking up with the database.
        /// </summary>
        /// <returns>
        /// A success message indicating the file was uploaded.
        /// </returns>
        [HttpPost(Name = "UploadFile")]
        public ActionResult<string> UploadFile(string fileName)
        {
            var nextId = _context.FileHeaders.Max(x => x.Id) + 1;

            _context.FileHeaders.Add(new FileHeader()
            {
                Id = nextId,
                FileName = fileName,
                Created = DateTime.UtcNow,
                Status = "Pending"
            });

            return CreatedAtAction("GetFile", new { id = nextId }, fileName);
        }


        /// <summary>
        /// Updates a specific file by its ID. 
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was updated.
        /// </returns>
        [HttpPut("{id}", Name = "UpdateFile")]
        public ActionResult<string> UpdateFile(int id, string fileName, string status)
        {
            var file = _context.FileHeaders.FirstOrDefault(x => x.Id == id);
            if (file == null)
                return NotFound($"File Not Found: {id}");

            file.FileName = fileName;
            file.Status = status;

            return Ok(new
            {
                file.Id,
                file.FileName,
                file.Status
            });
        }

    }
}
