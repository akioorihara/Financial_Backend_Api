using FinancialBackendApi.Models.DTOs;

namespace FinancialBackendApi.Services
{
    /// <summary>
    /// Provides operations for retrieving and managing files.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Retrieves a paginated list of files.
        /// </summary>
        Task<IEnumerable<FileHeaderDto>> GetFilesAsync(int page, int pageSize);

        /// <summary>
        /// Retrieves a file and its associated details.
        /// </summary>
        /// <param name="fileId">The ID of the file.</param>
        Task<FileWithDetailsDto?> GetFileWithDetailsAsync(int fileId);

        /// <summary>
        /// Retrieves the details associated with a file.
        /// </summary>
        /// <param name="fileId">The ID of the file.</param>
        Task<IEnumerable<FileDetailDto?>> GetFileDetailDtosAsync(int fileId);

        /// <summary>
        /// Deletes a file and its associated details.
        /// </summary>
        /// <param name="fileId">The ID of the file.</param>
        Task<bool> DeleteFileAsync(int fileId);

        /// <summary>
        /// Imports a CSV file and returns the file header information.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<FileHeaderDto> ImportCsvAsync(IFormFile file);
    }
}
