using MicroNpmRegistry.Domain.Entities.Models;

namespace MicroNpmRegistry.Application.Queries.NpmQueries.GetPackageMetatDataQuery
{
    public class GetPackageMetaDataResult
    {
        public NpmPublishPayload Payload { get; set; }
    }
}
