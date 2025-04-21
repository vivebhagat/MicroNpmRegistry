using MediatR;
using MicroNpmRegistry.Infrastructure.Storage;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Queries.NpmQueries.DownloadPackageQuery
{
    public class DownloadPackageQueryHandler : IRequestHandler<DownloadPackageCommand, DownloadPackageResult>
    {
        private readonly ILogger _logger;
        private IFileService FileService { get; }

        public DownloadPackageQueryHandler(IFileService fileService)
        {
            FileService = fileService;             
        }

        public async Task<DownloadPackageResult> Handle(DownloadPackageCommand request, CancellationToken cancellationToken)
        {
            var _decodedFileName = WebUtility.UrlDecode(request.FileName);

            var filePath = FileService.GetPath(_decodedFileName);

            if (FileService.Exists(filePath))
                return new DownloadPackageResult { PackageFilePath = filePath };
           
            return null;
        }
    }
}
