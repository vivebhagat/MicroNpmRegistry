using MediatR;
using MicroNpmRegistry.Domain.Entities;

namespace Application.Queries.NpmQueries.GetPackageMetatDataQuery
{
    public class GetPackageMetaDataQueryRequest : IRequest<GetPackageMetaDataResult>
    {
        public string FileName { get; set; }
        public StorageConfiguration StorageConfiguration { get; set; }
    }
}
