using MediatR;
using MicroNpmRegistry.Domain.Entities.Models;
using Newtonsoft.Json;
using System.Net;

namespace MicroNpmRegistry.Application.Queries.NpmQueries.GetPackageMetatDataQuery
{
    public class GetPackageMetaDataQueryHandler : IRequestHandler<GetPackageMetaDataCommand, GetPackageMetaDataResult>
    {
        public async Task<GetPackageMetaDataResult> Handle(GetPackageMetaDataCommand request, CancellationToken cancellationToken)
        {
            var _decodedName = WebUtility.UrlDecode(request.FileName);

            var filePath = Path.Combine(request.LocalStoragePath, $"{_decodedName}.info");
            if (System.IO.File.Exists(filePath))
            {
                var metaDataString = System.IO.File.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<NpmPublishPayload>(metaDataString);
                return new GetPackageMetaDataResult { Payload = result };
            }
            return null;
        }
    }
}
