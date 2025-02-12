using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MicroNpmRegistry.Entities;
using MicroNpmRegistry.Entities.Models;
using System.Net;

namespace MicroNpmRegistry.Controllers
{

    [ApiController]
    [Route("registry")]
    public class RegistryController : ControllerBase
    {

        private readonly ILogger<RegistryController> _logger;
        private RegistrySettings registrySettings { get; set; }
        public RegistryController(ILogger<RegistryController> logger, IOptions<RegistrySettings> options)
        {
            _logger = logger;
            registrySettings = options?.Value;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadPackage(IFormFile file)
        {
            var filePath = Path.Combine(registrySettings.LocalStaoragePath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new { Message = "Package uploaded successfully!" });
        }

        [HttpGet("package/{name}")]
        public IActionResult GetPackageMetadata(string name)
        {
            var _decodedName = WebUtility.UrlDecode(name);
            var _name = _decodedName.Replace(@$"{registrySettings.OrganizationName}/", "");
            var filePath = Path.Combine(registrySettings.LocalStaoragePath, $"{_name}.info");
            if (System.IO.File.Exists(filePath))
            {
                var metaDataString = System.IO.File.ReadAllText(filePath);
                var s = JsonConvert.DeserializeObject<PackageMetadata>(metaDataString);
                return new JsonResult(s);
            }
            return NotFound();
        }

        [HttpGet("download/{name}")]
        public IActionResult DownloadPackage(string name)
        {
            var _decodedName = WebUtility.UrlDecode(name);
            name = _decodedName.Replace(@$"{registrySettings.OrganizationName}/", "");
            var filePath = Path.Combine(registrySettings.LocalStaoragePath, _decodedName);
            if (System.IO.File.Exists(filePath))
            {
                return PhysicalFile(filePath, "application/octet-stream");
            }
            return NotFound();
        }

    }
}
