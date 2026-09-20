using Microsoft.AspNetCore.Mvc;

namespace FinancialBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {


        /// <summary>
        /// Gets a list of available files.
        /// </summary>
        /// <returns>A list of file names.</returns>
        [HttpGet(Name = "GetFiles")]
        public IEnumerable<string> Get()
        {
            return new string[] { "File1.csv", "File2.csv", "File3.xlsx" };
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
        /// <param name="id">File id</param>
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
