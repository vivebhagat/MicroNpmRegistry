using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using MicroNpmRegistry.Domain.Entities;
using MicroNpmRegistry.Domain.Entities.Models;
using MicroNpmRegistry.Helper;
using MediatR;
using Application.Commands.NpmCommands.PublishPackage;
using Application.Queries.NpmQueries.GetPackageMetatDataQuery;
using Application.Queries.NpmQueries.DownloadPackageQuery;

namespace MicroNpmRegistry.Controllers
{
    [ApiController]
    [Route("artifactory/api/npm")]
    public class RegistryController : ControllerBase
    {

        private readonly ILogger<RegistryController> _logger;
        private RegistrySettings registrySettings { get; set; }
        private IMediator Mediator { get; set; }
        public RegistryController(ILogger<RegistryController> logger, IOptions<RegistrySettings> options, IMediator _mediator)
        {
            _logger = logger;
            registrySettings = options?.Value;
            Mediator = _mediator;
        }



        [HttpPut("{orgname}%2f{filename}")]
        public async Task<IActionResult> UploadPackage(string orgname,string filename)
        {
            if (orgname != registrySettings.OrganizationName)
                return new BadRequestResult();

            string body = await HttpHelper.GetRequestBodyAsync(Request);
            // Deserialize manually
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var payload = JsonSerializer.Deserialize<NpmPublishPayload>(body, options);

            var result = await Mediator.Send(new PublishPackageCommandRequest { Payload =  payload });
           
            if(result == null)
                return new BadRequestResult();

            return Ok(new { success = true });
        }


        [HttpGet("{orgname}%2f{name}")]
        public async Task<IActionResult> GetPackageMetadata(string orgname,string name)
        {
            if (orgname != registrySettings.OrganizationName)
                return new BadRequestResult();

            var result = await Mediator.Send(new GetPackageMetaDataQueryRequest {
                FileName = name
            });

            if(result == null)
                return NotFound();

            return new JsonResult(result?.Payload);
        }


        [HttpGet("{orgname}/{name}/-/{_orgname}/{filename}")]
        public async Task<IActionResult> DownloadPackage(string orgname, string name, string _orgname, string filename)
        {
            if (orgname == registrySettings.OrganizationName || _orgname != registrySettings.OrganizationName)
                return new BadRequestResult();

            var result = await Mediator.Send(new DownloadPackageQueryRequest { FileName  = filename, 
                LocalStoragePath = registrySettings.LocalStaoragePath });

            if(result == null) 
                return BadRequest();

            if(string.IsNullOrEmpty(result.PackageFilePath))
                return NotFound();

           return PhysicalFile(result.PackageFilePath, "application/octet-stream");

        }

    }
}
