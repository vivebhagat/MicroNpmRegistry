using MediatR;

namespace MicroNpmRegistry.Application.Queries.NpmQueries.DownloadPackageQuery
{
    public class DownloadPackageCommand : IRequest<DownloadPackageResult>
    {
        public string FileName { get; set; }
        public string LocalStoragePath { get; set; }
    }
}
