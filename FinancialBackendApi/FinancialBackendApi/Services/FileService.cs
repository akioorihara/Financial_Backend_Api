using FinancialBackendApi.Models.DTOs;

namespace FinancialBackendApi.Services
{
    public class FileService : IFileService
    {
        Task IFileService.DeleteFileAsync(int fileId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<FileDetailDto>> IFileService.GetFileDetailDtosAsync(int fileId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<FileHeaderDto>> IFileService.GetFilesAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task<FileWithDetailsDto> IFileService.GetFileWithDetailsAsync(int fileId)
        {
            throw new NotImplementedException();
        }
    }
}
