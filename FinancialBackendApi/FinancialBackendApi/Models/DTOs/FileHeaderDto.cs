namespace FinancialBackendApi.Models.DTOs
{
    public class FileHeaderDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = null;
        public string? Status { get; set; } = "Pending";

    }
}
