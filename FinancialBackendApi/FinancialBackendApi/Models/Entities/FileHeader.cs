namespace FinancialBackendApi.Models.Entities
{
    public class FileHeader
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";
        public ICollection<FileDetail> Details { get; set; }
            = new List<FileDetail>();
    }
}
