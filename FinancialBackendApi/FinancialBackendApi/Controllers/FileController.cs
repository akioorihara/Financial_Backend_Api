using FinancialBackendApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// A static list of file headers to simulate a database.
        /// </summary>
        private static readonly FileHeader[] _files = new[]
        {
            new FileHeader { Id = 1, FileName = "File1.csv", Created = DateTime.UtcNow, Status = "Processed" },
            new FileHeader { Id = 2, FileName = "File2.csv", Created = DateTime.UtcNow, Status = "Pending" },
            new FileHeader { Id = 3, FileName = "File3.xlsx", Created = DateTime.UtcNow, Status = "Failed" }
        };


        /// <summary>
        /// A static list of file details to simulate a database.
        /// </summary>
        private static readonly FileDetail[] _fileDetails = new[]
        {
            new FileDetail { Id = 1, FileHeaderId = 1, Date = DateOnly.FromDateTime(DateTime.UtcNow), Amount = 100.0m, Description = "Transaction 1", Type = "Credit", Category = "Sales", Account = "Account1" },
            new FileDetail { Id = 2, FileHeaderId = 1, Date = DateOnly.FromDateTime(DateTime.UtcNow), Amount = -50.0m, Description = "Transaction 2", Type = "Debit", Category = "Refunds", Account = "Account2" },
            new FileDetail { Id = 3, FileHeaderId = 2, Date = DateOnly.FromDateTime(DateTime.UtcNow), Amount = 200.0m, Description = "Transaction 3", Type = "Credit", Category = "Sales", Account = "Account1" },
            new FileDetail { Id = 4, FileHeaderId = 3, Date = DateOnly.FromDateTime(DateTime.UtcNow), Amount = -75.0m, Description = "Transaction 4", Type = "Debit", Category = "Refunds", Account = "Account2" }
        };



        /// <summary>
        /// Gets a list of available files.
        /// </summary>
        /// <returns>A list of file names.</returns>
        [HttpGet(Name = "GetFiles")]
        public IEnumerable<FileHeader> Get()
        {
            return _files.OrderByDescending(x => x.Id).Take(100);
        }


        /// <summary>
        /// Gets a specific file by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was retrieved.
        /// </returns>
        [HttpGet("{id}", Name = "GetFile")]
        public ActionResult<string> GetFile(int id)
        {
            return Ok($"File: {id}");
        }


        /// <summary>
        /// Gets the details of a specific file by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A list of file details.
        /// </returns>
        [HttpGet("{id}/details", Name = "GetFileDetails")]
        public IEnumerable<FileDetail> GetFileDetails(int id)
        {
            return _fileDetails
                .Where(_fileDetails => _fileDetails.FileHeaderId == id)
                .OrderByDescending(x => x.Id)
                .Take(100);
        }


        /// <summary>
        /// Deletes a specific file by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was deleted.
        /// </returns>
        [HttpDelete("{id}", Name = "DeleteFile")]
        public ActionResult<string> DeleteFile(int id)
        {
            return Ok($"File deleted: {id}");
        }


        /// <summary>
        /// Uploads a new file to the server.
        /// </summary>
        /// <returns>
        /// A success message indicating the file was uploaded.
        /// </returns>
        [HttpPost(Name = "UploadFile")]
        public ActionResult<string> UploadFile()
        {
            return Ok("File uploaded");
        }


        /// <summary>
        /// Updates a specific file by its ID.
        /// </summary>
        /// <param name="id">File ID</param>
        /// <returns>
        /// A success message indicating the file was updated.
        /// </returns>
        [HttpPut("{id}", Name = "UpdateFile")]
        public ActionResult<string> UpdateFile(int id)
        {
            return Ok($"File updated: {id}");
        }

    }
}
