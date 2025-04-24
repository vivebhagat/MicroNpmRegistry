using CQRS;
using CQRS.Commands;
using MediatR;
using MicroNpmRegistry.Infrastructure.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Commands.NpmCommands.PublishPackage
{
    public class PublishPackageCommandHandler(ILogger<PublishPackageCommandHandler> logger, IFileService fileService) : IRequestHandler<PublishPackageCommandRequest, PublishPackageResult>, IApplicationHandler
    {
        private readonly ILogger<PublishPackageCommandHandler> Logger = logger;
        private IFileService FileService { get; } = fileService;

        public async Task<PublishPackageResult> Handle(PublishPackageCommandRequest request, CancellationToken cancellationToken)
        {
            if (request?.Payload is not { Name: not null, Attachments: not null })
                return null;
            
            var packageId = request.Payload.Id;
            var tarballData = request.Payload.Attachments.First().Value.Base64Data;

            var bytes = Convert.FromBase64String(tarballData);

            var tarBallFilePath = FileService.GetPath($"{request.Payload.Name}.tgz");
            var metaDataFilePath = FileService.GetPath($"{request.Payload.Name}.info");

            var saveTarTask = FileService.WriteAllByteAsync(tarBallFilePath, bytes);
            var saveMetaDataTask = FileService.WriteAllTextAsync(metaDataFilePath, JsonConvert.SerializeObject(request.Payload));

            try
            {
                await Task.WhenAll(saveTarTask, saveMetaDataTask);
            }
            catch
            {
                try
                {
                    FileService.DeleteFile(tarBallFilePath);
                    FileService.DeleteFile(metaDataFilePath);
                }
                finally
                {
                }
                return null;
            }

            return new PublishPackageResult();
            
        }
    }
}
