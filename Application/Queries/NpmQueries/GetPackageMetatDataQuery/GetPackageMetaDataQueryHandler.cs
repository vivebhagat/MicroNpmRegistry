using MediatR;
using MicroNpmRegistry.Domain.Entities.Models;
using MicroNpmRegistry.Infrastructure.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Application.Queries.NpmQueries.GetPackageMetatDataQuery
{
    public class GetPackageMetaDataQueryHandler : IRequestHandler<GetPackageMetaDataCommand, GetPackageMetaDataResult>
    {
        private IFileService FileService { get; set; }
        private ILogger<GetPackageMetaDataQueryHandler> Logger { get; set; }
        public GetPackageMetaDataQueryHandler(IFileService fileService)
        {
            FileService = fileService;
        }

        public async Task<GetPackageMetaDataResult> Handle(GetPackageMetaDataCommand request, CancellationToken cancellationToken)
        {
            var _decodedName = WebUtility.UrlDecode(request.FileName);

            var filePath = FileService.GetPath( $"{_decodedName}.info");
            if (FileService.Exists(filePath))
            {
                var metaDataString = FileService.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<NpmPublishPayload>(metaDataString);
                return new GetPackageMetaDataResult { Payload = result };
            }
            return null;
        }
    }
}
