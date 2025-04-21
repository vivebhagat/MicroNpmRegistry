using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MicroNpmRegistry.Entities;
using MicroNpmRegistry.Entities.Models;
using System.Net;
using System.Text.Json;
using System.Text;

namespace MicroNpmRegistry.Controllers
{
    [ApiController]
    [Route("artifactory/api/npm")]
    public class RegistryController : ControllerBase
    {

        private readonly ILogger<RegistryController> _logger;
        private RegistrySettings registrySettings { get; set; }
        public RegistryController(ILogger<RegistryController> logger, IOptions<RegistrySettings> options)
        {
            _logger = logger;
            registrySettings = options?.Value;
        }


        [HttpPut("{orgname}%2f{filename}")]
        public async Task<IActionResult> UploadPackage(string orgname,string filename)
        {
            if (orgname != registrySettings.OrganizationName)
                return new BadRequestResult();

            // Enable buffering so we can read the request body multiple times
            Request.EnableBuffering();

            // Read the raw body string
            string body;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
            }

            // Reset the stream position so other middleware can still read it
            Request.Body.Position = 0;

            // Deserialize manually
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var payload = System.Text.Json.JsonSerializer.Deserialize<NpmPublishPayload>(body, options);

            var packageId = payload.Id;
            var versionData = payload.Versions.First().Value;
            var tarballData = payload.Attachments.First().Value.Base64Data;

            // Optionally decode and save tarball
            var bytes = Convert.FromBase64String(tarballData);
             var filePath = Path.Combine(registrySettings.LocalStaoragePath, filename);

            System.IO.File.WriteAllBytes(filePath +".tgz", bytes);
            System.IO.File.WriteAllText(filePath + ".info" , JsonConvert.SerializeObject(payload));

            return Ok(new { success = true });
        }

        [HttpGet("{orgname}%2f{name}")]
        public IActionResult GetPackageMetadata(string orgname,string name)
        {
            if (orgname != registrySettings.OrganizationName)
                return new BadRequestResult();

            var _decodedName = WebUtility.UrlDecode(name);

            var filePath = Path.Combine(registrySettings.LocalStaoragePath, $"{_decodedName}.info");
            if (System.IO.File.Exists(filePath))
            {
                var metaDataString = System.IO.File.ReadAllText(filePath);
                var s = JsonConvert.DeserializeObject<NpmPublishPayload>(metaDataString);
                return new JsonResult(s);
            }
            return NotFound();
        }

        [HttpGet("{orgname}/{name}/-/{_orgname}/{filename}")]
        public IActionResult DownloadPackage(string orgname, string name, string _orgname, string filename)
        {
            if (orgname == registrySettings.OrganizationName || _orgname != registrySettings.OrganizationName)
                return new BadRequestResult();

            var _decodedFileName = WebUtility.UrlDecode(filename);

            var filePath = Path.Combine(registrySettings.LocalStaoragePath, _decodedFileName);
            if (System.IO.File.Exists(filePath))
            {
                return PhysicalFile(filePath, "application/octet-stream");
            }
            return NotFound();
        }

    }
}
