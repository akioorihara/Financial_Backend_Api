using FinancialBackendApi.Data;
using FinancialBackendApi.Models.DTOs;
using FinancialBackendApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialBackendApi.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger instance.</param>
        private readonly FinancialDbContext _context;
        private readonly ILogger<FileService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger instance.</param>
        public FileService(FinancialDbContext context, ILogger<FileService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Gets a paginated list of file headers.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>The list of file headers.</returns>
        public async Task<IEnumerable<FileHeaderDto>> GetFilesAsync(int page, int pageSize)
        {
            return await _context.FileHeaders
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new FileHeaderDto
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    Created = x.Created,
                    Status = x.Status
                })
                .ToListAsync();
        }

        /// <summary>
        /// Deletes a file by its ID.
        /// </summary>
        /// <param name="fileId">The ID of the file to delete.</param>
        /// <returns>A boolean indicating whether the file was deleted.</returns>
        public async Task<bool> DeleteFileAsync(int fileId)
        {
            var file = await _context.FileHeaders.SingleOrDefaultAsync(x => x.Id == fileId);

            if (file == null)
                return false;

            _context.FileHeaders.Remove(file);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Gets the details of a file by its ID.
        /// </summary>
        /// <param name="fileId">The file ID.</param>
        /// <returns>The file details.</returns>
        public async Task<IEnumerable<FileDetailDto?>> GetFileDetailDtosAsync(int fileId)
        {
            return await _context
                .FileDetails
                .Where(x => x.FileHeaderId == fileId)
                .Select(x => new FileDetailDto
                {
                    Id = x.Id,
                    FileHeaderId = x.FileHeaderId,
                    Date = x.Date,
                    Amount = x.Amount,
                    Description = x.Description,
                    Type = x.Type,
                    Category = x.Category,
                    Account = x.Account

                }).ToListAsync();

        }


        /// <summary>
        /// Gets a file with its details by file ID.
        /// </summary>
        /// <param name="fileId">The file ID.</param>
        /// <returns>The file with details.</returns>
        public async Task<FileWithDetailsDto?> GetFileWithDetailsAsync(int fileId)
        {

            var file = await _context.FileHeaders
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == fileId);

            if (file == null)
            {
                return null;
            }

            return (new FileWithDetailsDto
            {
                FileHeader = new FileHeaderDto
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    Created = file.Created,
                    Status = file.Status

                },
                FileDetails = file.Details.Select(x => new FileDetailDto
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

            });
        }

        /// <summary>
        /// TODO - Imports a CSV file and returns the file header and associated FileDetailsDTOs.
        /// </summary>
        /// <param name="file">IFormFile </param>
        /// <returns>FileHeaderDto</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<FileHeaderDto> ImportCsvAsync(IFormFile file)
        {

            if (!file.FileName.Contains(".csv", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("The provided file is not a CSV file.");
            }

            // Parse the CSV file and create a new FileHeaderDto and associated FileDetailsDto

            _context.FileHeaders.Add(new FileHeader
            {
                Created = DateTime.UtcNow,
                Status = "Pending",
                Details = new List<FileDetail>
                {

                }
            });




            return Task.FromResult(new FileHeaderDto
            {
                Id = 0, // Placeholder ID, should be replaced with actual ID after saving to the database
                FileName = file.FileName,
                Created = DateTime.UtcNow,
                Status = "Pending"
            });

            //_context.FileHeaders.Add(new FileHeader()
            //{
            //    Id = nextId,
            //    FileName = file.FileName,
            //    Created = DateTime.UtcNow,
            //    Status = "Pending"
            //});

            //return CreatedAtAction("GetFile", new { id = nextId }, file);

        }
    }
}
