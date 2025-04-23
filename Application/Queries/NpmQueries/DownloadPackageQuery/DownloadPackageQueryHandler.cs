using MediatR;
using MicroNpmRegistry.Infrastructure.Storage;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Queries.NpmQueries.DownloadPackageQuery
{
    public class DownloadPackageQueryHandler(ILogger<DownloadPackageQueryHandler> logger, IFileService _fileService) : IRequestHandler<DownloadPackageQueryRequest, DownloadPackageResult>
    {
        private readonly ILogger<DownloadPackageQueryHandler> Logger = logger;
        private IFileService FileService { get; } = _fileService;

        public async Task<DownloadPackageResult> Handle(DownloadPackageQueryRequest request, CancellationToken cancellationToken)
        {
            if (request is not { FileName: not null, LocalStoragePath: not null })
                return null;

            var _decodedFileName = WebUtility.UrlDecode(request.FileName);

            var filePath = FileService.GetPath(_decodedFileName);

            if (FileService.Exists(filePath))
                return new DownloadPackageResult { PackageFilePath = filePath };
           
            return new DownloadPackageResult();
        }
    }
}
