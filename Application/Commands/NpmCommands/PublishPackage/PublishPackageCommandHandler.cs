using MediatR;
using MicroNpmRegistry.Infrastructure.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Commands.NpmCommands.PublishPackage
{
    public class PublishPackageCommandHandler : IRequestHandler<PublishPackageCommand, PublishPackageResult>
    {
        private readonly ILogger _logger;
        private IFileService FileService { get; }

        public PublishPackageCommandHandler(IFileService fileService)
        {
            FileService = fileService;            
        }

        public async Task<PublishPackageResult> Handle(PublishPackageCommand request, CancellationToken cancellationToken)
        {
            var packageId = request.Payload.Id;
            var versionData = request.Payload.Versions.First().Value;
            var tarballData = request.Payload.Attachments.First().Value.Base64Data;

            // Optionally decode and save tarball
            var bytes = Convert.FromBase64String(tarballData);
            var filePath = FileService.GetPath(request.fileName);

            FileService.WriteAllBytes(filePath + ".tgz", bytes);
            FileService.WriteAllText(filePath + ".info", JsonConvert.SerializeObject(request.Payload));
            return new PublishPackageResult();
            
        }
    }
}
