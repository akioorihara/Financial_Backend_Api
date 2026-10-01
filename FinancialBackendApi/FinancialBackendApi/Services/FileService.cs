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

        public async Task<IEnumerable<FileDetailDto>> GetFileDetailDtosAsync(int fileId)
        {
            throw new NotImplementedException();
        }



        public async Task<FileWithDetailsDto> GetFileWithDetailsAsync(int fileId)
        {
            throw new NotImplementedException();
        }
    }
}
