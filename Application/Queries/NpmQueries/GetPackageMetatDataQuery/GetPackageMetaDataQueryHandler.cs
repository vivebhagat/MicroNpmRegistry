using CQRS;
using MediatR;
using MicroNpmRegistry.Domain.Entities.Models;
using MicroNpmRegistry.Infrastructure.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Application.Queries.NpmQueries.GetPackageMetatDataQuery
{
    public class GetPackageMetaDataQueryHandler(ILogger<GetPackageMetaDataQueryHandler> logger, IFileService fileService) : IRequestHandler<GetPackageMetaDataQueryRequest, GetPackageMetaDataResult>, IApplicationHandler
    {
        private readonly ILogger<GetPackageMetaDataQueryHandler> Logger = logger;
        private IFileService FileService { get; set; } = fileService;

        public async Task<GetPackageMetaDataResult> Handle(GetPackageMetaDataQueryRequest request, CancellationToken cancellationToken)
        {
            if (request is not { FileName: not null, StorageConfiguration : not null })
                return null;

            var _decodedName = WebUtility.UrlDecode(request.FileName);

            var filePath = FileService.GetPath($"{_decodedName}.info");
            if (FileService.Exists(filePath))
            {
                var metaDataString = FileService.ReadAllText(filePath);
                try
                {
                    var result = JsonConvert.DeserializeObject<NpmPublishPayload>(metaDataString);

                    if(result is not { Id: not null, Name: not null })
                        return null;

                    return new GetPackageMetaDataResult { Payload = result };
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}
