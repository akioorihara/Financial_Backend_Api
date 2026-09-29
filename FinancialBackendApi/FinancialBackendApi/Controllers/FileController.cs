using FinancialBackendApi.Data;
using FinancialBackendApi.Models;
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
        public async Task<ActionResult<IEnumerable<FileHeaderDto>>> Get(int page = 1, int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize must be greater than 0.");
            }

            return await _context.FileHeaders
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new FileHeaderDto
                {
                    FileName = x.FileName,
                    Created = x.Created,
                    Status = x.Status
                })
                .ToListAsync();
        }


        /// <summary>
        /// Gets a specific file by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was retrieved.
        /// </returns>
        [HttpGet("{id}", Name = "GetFile")]
        public async Task<ActionResult<string>> GetFile(int id)
        {
            var file = await _context.FileHeaders.FirstOrDefaultAsync(x => x.Id == id);
            var fileDetails = await _context.FileDetails.Where(x => x.FileHeaderId == id).ToListAsync();

            if (file != null)
            {
                return Ok(new
                {
                    FileWithDetailsDto = new FileWithDetailsDto
                    {
                        FileHeader = new FileHeaderDto
                        {
                            FileName = file.FileName,
                            Created = file.Created,
                            Status = file.Status
                        },
                        FileDetails = fileDetails.Select(x => new FileDetailDto
                        {
                            Id = x.Id,
                            FileHeaderId = x.FileHeaderId,
                            Date = x.Date,
                            Amount = x.Amount,
                            Description = x.Description,
                            Type = x.Type,
                            Category = x.Category,
                            Account = x.Account

                        }).ToList()
                    }
                });
            }

            return NotFound($"File not found; {id}");
        }


        /// <summary>
        /// Gets the detail object by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A list of file details.
        /// </returns>
        [HttpGet("{id}/details", Name = "GetFileDetails")]
        public async Task<ActionResult<IEnumerable<FileDetail>>> GetFileDetails(int id)
        {
            var file = await _context.FileHeaders.FirstOrDefaultAsync(x => x.Id == id);
            if (file == null)
                return NotFound($"File not found: ID - {id}");

            return await _context.FileDetails
                .Where(x => x.FileHeaderId == id)
                .OrderByDescending(x => x.Id)
                .Take(100).ToListAsync();
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
            var file = await _context.FileHeaders.FirstOrDefaultAsync(x => x.Id == id);

            if (file == null)
                return NotFound($"File not found: ID - {id}");

            // remove the file and its details from the database
            _context.FileHeaders.Remove(file);

            // save changes to the database
            await _context.SaveChangesAsync();

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
