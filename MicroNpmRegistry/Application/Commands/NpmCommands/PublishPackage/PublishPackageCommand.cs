using MediatR;
using MicroNpmRegistry.Domain.Entities.Models;

namespace MicroNpmRegistry.Application.Commands.NpmCommands.PublishPackage
{
    public class PublishPackageCommand: IRequest<PublishPackageResult>
    {
        public NpmPublishPayload Payload { get;  set; }
        public string LocalStoragePath { get; set; }
        public string fileName { get; set; }
    }
}
