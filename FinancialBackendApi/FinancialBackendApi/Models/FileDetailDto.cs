namespace FinancialBackendApi.Models
{
    public class FileDetailDto
    {
        public string? AccountNumber { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }

    }
}
