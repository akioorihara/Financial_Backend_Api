namespace FinancialBackendApi.Models
{
    public class FileHeader
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";
        //public ICollection<FileDetail> Details { get; set; }
        //    = new List<FileDetail>();
    }
}
