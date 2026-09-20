namespace FinancialBackendApi.Models
{
    public class FileDetail
    {
        public int Id { get; set; }
        public int FileHeaderId { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public FileHeader FileHeader { get; set; } = new FileHeader();
    }
}
