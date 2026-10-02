using FinancialBackendApi.Data;
using FinancialBackendApi.Models.DTOs;
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

        public async Task DeleteFileAsync(int fileId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the details of a file by its ID.
        /// </summary>
        /// <param name="fileId">The file ID.</param>
        /// <returns>The file details.</returns>
        public async Task<IEnumerable<FileDetailDto>> GetFileDetailDtosAsync(int fileId)
        {

            var file = await _context.FileHeaders.FirstOrDefaultAsync(x => x.Id == fileId);
            if (file == null)
            {
                _logger.LogWarning($"File with ID {fileId} not found.");
                return null;
            }

            return await _context.FileDetails.Select(x => new FileDetailDto
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
    }
}
