using MediatR;
using Newtonsoft.Json;

namespace MicroNpmRegistry.Application.Commands.NpmCommands.PublishPackage
{
    public class PublishPackageCommandHandler : IRequestHandler<PublishPackageCommand, PublishPackageResult>
    {
        public async Task<PublishPackageResult> Handle(PublishPackageCommand request, CancellationToken cancellationToken)
        {
            var packageId = request.Payload.Id;
            var versionData = request.Payload.Versions.First().Value;
            var tarballData = request.Payload.Attachments.First().Value.Base64Data;

            // Optionally decode and save tarball
            var bytes = Convert.FromBase64String(tarballData);
            var filePath = Path.Combine(request.LocalStoragePath, request.fileName);

            File.WriteAllBytes(filePath + ".tgz", bytes);
            File.WriteAllText(filePath + ".info", JsonConvert.SerializeObject(request.Payload));
            return new PublishPackageResult();
            
        }
    }
}
