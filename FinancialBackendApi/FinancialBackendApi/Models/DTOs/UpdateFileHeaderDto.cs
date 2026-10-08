namespace FinancialBackendApi.Models.DTOs
{
    /// <summary>
    /// Represents the data transfer object for updating a file header.
    /// </summary>
    public class UpdateFileHeaderDto
    {
        /// <summary>
        /// Gets or sets the status of the file header.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
