using MediatR;
using MicroNpmRegistry.Domain.Entities;
using MicroNpmRegistry.Domain.Entities.Models;
using Newtonsoft.Json;
using System.Net;

namespace MicroNpmRegistry.Application.Queries.NpmQueries.DownloadPackageQuery
{
    public class DownloadPackageQueryHandler : IRequestHandler<DownloadPackageCommand, DownloadPackageResult>
    {
        public async Task<DownloadPackageResult> Handle(DownloadPackageCommand request, CancellationToken cancellationToken)
        {
            var _decodedFileName = WebUtility.UrlDecode(request.FileName);

            var filePath = Path.Combine(request.LocalStoragePath, _decodedFileName);

            if (System.IO.File.Exists(filePath))
                return new DownloadPackageResult { PackageFilePath = filePath };
           
            return null;
        }
    }
}
