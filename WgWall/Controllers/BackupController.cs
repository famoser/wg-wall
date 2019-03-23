using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WgWall.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        [HttpGet]
        public IActionResult Info()
        {
            return new OkObjectResult("available nodes are GET /Backup/db.sqlite, GET /Backup/db.sqlite_backup and POST /Backup/post. Be careful!");
        }

        [HttpGet("db.sqlite")]
        public async Task<IActionResult> Download()
        {
            var bytes = await System.IO.File.ReadAllBytesAsync(Startup.DbFileName);
            return new FileContentResult(bytes, "application/octet-stream");
        }

        [HttpGet("db.sqlite_backup")]
        public async Task<IActionResult> DownloadBackup()
        {
            var bytes = await System.IO.File.ReadAllBytesAsync(Startup.DbFileName);
            return new FileContentResult(bytes, "application/octet-stream");
        }

        [HttpPost("post")]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                return new OkObjectResult("no file found in the request :(");
            }

            System.IO.File.Copy(Startup.DbFileName, Startup.DbFileName + "_backup", true);
            System.IO.File.Delete(Startup.DbFileName);
            using (var stream = new FileStream(Startup.DbFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return new OkObjectResult("true. was that the wrong file? you can download the backup from /Backup/db.sqlite_backup");
        }
    }
}