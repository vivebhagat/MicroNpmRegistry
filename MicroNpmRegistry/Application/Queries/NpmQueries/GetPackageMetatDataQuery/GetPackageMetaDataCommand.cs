using MediatR;

namespace MicroNpmRegistry.Application.Queries.NpmQueries.GetPackageMetatDataQuery
{
    public class GetPackageMetaDataCommand : IRequest<GetPackageMetaDataResult>
    {
        public string FileName { get; set; }
        public string LocalStoragePath { get; set; }
    }
}
