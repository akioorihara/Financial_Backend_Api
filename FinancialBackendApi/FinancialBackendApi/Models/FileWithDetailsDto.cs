namespace FinancialBackendApi.Models
{
    /// <summary>
    /// Represents a file with its details.
    /// DTO (Data Transfer Object) used for transferring file information along with its associated details.
    /// </summary>
    public class FileWithDetailsDto
    {
        public FileHeaderDto FileHeader { get; set; } = new FileHeaderDto();
        public FileDetailDto FileDetail { get; set; } = new FileDetailDto();
    }
}
