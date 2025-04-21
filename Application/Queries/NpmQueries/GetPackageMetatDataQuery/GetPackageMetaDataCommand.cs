using MediatR;
using MicroNpmRegistry.Domain.Entities;

namespace Application.Queries.NpmQueries.GetPackageMetatDataQuery
{
    public class GetPackageMetaDataCommand : IRequest<GetPackageMetaDataResult>
    {
        public string FileName { get; set; }
        public StorageConfiguration StorageConfiguration { get; set; }
    }
}
