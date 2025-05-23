﻿using MediatR;
using MicroNpmRegistry.Domain.Entities;
using MicroNpmRegistry.Domain.Entities.Models;

namespace Application.Commands.NpmCommands.PublishPackage
{
    public class PublishPackageCommandRequest: IRequest<PublishPackageResult>
    {
        public string OrgnizationName { get; set; }
        public NpmPublishPayload Payload { get;  set; }
        public StorageConfiguration StorageConfiguration { get; set; }
    }
}
