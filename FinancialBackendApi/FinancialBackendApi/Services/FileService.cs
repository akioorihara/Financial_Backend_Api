using FinancialBackendApi.Data;
using FinancialBackendApi.Models.DTOs;
using FinancialBackendApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialBackendApi.Services
{
    /// <summary>
    /// Represents a service for managing file operations, 
    /// including importing CSV files, retrieving file headers and details, 
    /// and updating file statuses.
    /// </summary>
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
                    Updated = x.Updated,
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
        /// Imports a CSV file and returns the file header and associated FileDetailsDTOs.
        /// </summary>
        /// <param name="file">IFormFile </param>
        /// <returns>FileHeaderDto</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<FileHeaderDto> ImportCsvAsync(IFormFile file)
        {
            // Check if a file with the same name already exists in the database
            var existingFile = await _context.FileHeaders
                .FirstOrDefaultAsync(f => f.FileName == file.FileName);

            if (existingFile != null)
            {
                throw new InvalidOperationException($"A file with the name '{file.FileName}' already exists.");
            }

            // Validate the file extension and size
            var fileExtention = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (fileExtention != ".csv" || file.Length == 0)
            {
                throw new ArgumentException("The provided file contains invalid data.");
            }

            // Parse the CSV file and create a new FileHeaderDto and associated FileDetailsDto
            using var reader = new StreamReader(file.OpenReadStream());
            var headerLine = reader.ReadLine(); // Read the header line

            var details = new List<FileDetail>();

            string? line;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue; // Skip empty lines 
                }

                var values = line.Split(",");

                if (values == null || values.Length != 6)
                {
                    throw new InvalidOperationException($"CSV line should be 6 values separated by commas but contains {values?.Length ?? 0}");
                }

                var fileDetail = new FileDetail
                {
                    Date = DateOnly.TryParse(values?[0]
                        ?? throw new InvalidOperationException("Date value is missing."), out var date)
                        ? date : throw new InvalidOperationException("Invalid date format."),
                    Amount = decimal.TryParse(values?[1]
                        ?? throw new InvalidOperationException("Amount value is missing."), out var amount)
                        ? amount : throw new InvalidOperationException("Invalid amount format."),
                    Description = values?[2]
                        ?? throw new InvalidOperationException("Description value is missing."),
                    Type = values?[3]
                        ?? throw new InvalidOperationException("Type value is missing."),
                    Category = values?[4]
                        ?? throw new InvalidOperationException("Category value is missing."),
                    Account = values?[5]
                        ?? throw new InvalidOperationException("Account value is missing.")
                };
                details.Add(fileDetail);
            }


            var fileHeader = new FileHeader
            {
                FileName = file.FileName,
                Created = DateTime.UtcNow,
                Status = "Pending",
                Updated = null,
                Details = details
            };

            _context.FileHeaders.Add(fileHeader);
            await _context.SaveChangesAsync();

            // After saving, the fileHeader.Id will be populated with the new ID from the database
            return new FileHeaderDto
            {
                Id = fileHeader.Id,
                FileName = fileHeader.FileName,
                Created = fileHeader.Created,
                Status = fileHeader.Status
            };

        }

        /// <summary>
        /// Updates the status of a file header by its ID.
        /// </summary>
        /// <param name="fileHeaderId">File ID</param>
        /// <param name="status">The new status.</param>
        /// <returns>
        /// The updated file header.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<FileHeaderDto?> UpdateFileHeaderAsync(int fileHeaderId, string status)
        {

            var fileHeader = await _context.FileHeaders.FirstOrDefaultAsync(f => f.Id == fileHeaderId);

            if (fileHeader == null)
            {
                throw new InvalidOperationException($"File header not found : {fileHeaderId}");
            }

            fileHeader.Status = status;
            fileHeader.Updated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new FileHeaderDto
            {
                Id = fileHeader.Id,
                FileName = fileHeader.FileName,
                Created = fileHeader.Created,
                Updated = fileHeader.Updated,
                Status = fileHeader.Status
            };
        }
    }
}
