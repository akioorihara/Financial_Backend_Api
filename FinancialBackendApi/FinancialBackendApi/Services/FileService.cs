using FinancialBackendApi.Models.DTOs;

namespace FinancialBackendApi.Services
{
    public class FileService : IFileService
    {
        public async Task DeleteFileAsync(int fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FileDetailDto>> GetFileDetailDtosAsync(int fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FileHeaderDto>> GetFilesAsync(int page, int pageSize)
        {
            //_context.FileHeaders
            //    .OrderByDescending(x => x.Id)
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .Select(x => new FileHeaderDto
            //    {
            //        Id = x.Id,
            //        FileName = x.FileName,
            //        Created = x.Created,
            //        Status = x.Status
            //    })
            //    .ToListAsync();

            return null;

            //throw new NotImplementedException();
        }

        public async Task<FileWithDetailsDto> GetFileWithDetailsAsync(int fileId)
        {
            throw new NotImplementedException();
        }
    }
}
