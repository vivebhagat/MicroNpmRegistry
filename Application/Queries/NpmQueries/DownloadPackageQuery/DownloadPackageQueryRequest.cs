using MediatR;

namespace Application.Queries.NpmQueries.DownloadPackageQuery
{
    public class DownloadPackageQueryRequest : IRequest<DownloadPackageResult>
    {
        public string FileName { get; set; }
        public string LocalStoragePath { get; set; }
    }
}
