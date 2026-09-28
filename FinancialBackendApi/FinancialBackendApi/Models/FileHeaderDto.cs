namespace FinancialBackendApi.Models
{
    public class FileHeaderDto
    {
        public string FileName { get; set; } = string.Empty;
        public string? Status { get; set; } = "Pending";
    }
}
