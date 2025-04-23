using Application.Commands.NpmCommands.PublishPackage;
using MicroNpmRegistry.Domain.Entities.Models;
using MicroNpmRegistry.Infrastructure.Storage;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests
{
    public class PublishPackageCommandTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_NullRequestHandler_ReturnsNull()
        {
            var logger = new Mock<ILogger<PublishPackageCommandHandler>>().Object;
            IFileService service = new Mock<IFileService>().Object;

            var publishPackageCommandHandler = new PublishPackageCommandHandler(logger, service);
            var result = await publishPackageCommandHandler.Handle(null,  CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_EmptyRequestHandler_ReturnsNull()
        {
            var logger = new Mock<ILogger<PublishPackageCommandHandler>>().Object;
            IFileService service = new Mock<IFileService>().Object;

            var publishPackageCommandHandler = new PublishPackageCommandHandler(logger, service);
            var result = await publishPackageCommandHandler.Handle(new PublishPackageCommandRequest(), CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_MissingAttachmentRequestHandler_ReturnsNull()
        {
            var logger = new Mock<ILogger<PublishPackageCommandHandler>>().Object;
            IFileService service = new Mock<IFileService>().Object;


            var input = new PublishPackageCommandRequest()
            {
                Payload = new NpmPublishPayload
                {
                    Id = "123",
                    Name = "Test"                    
                }
            };

            var publishPackageCommandHandler = new PublishPackageCommandHandler(logger, service);
            var result = await publishPackageCommandHandler.Handle(input, CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_ValidAttachmentContentRequestHandler_ReturnsSuccess()
        {
            var logger = new Mock<ILogger<PublishPackageCommandHandler>>().Object;
            IFileService service = new Mock<IFileService>().Object;

            var input = new PublishPackageCommandRequest()
            {
                Payload = new NpmPublishPayload
                {
                    Id = "123",
                    Name = "Test",
                    Attachments = new Dictionary<string, NpmAttachment>
                    {
                        {  "test", new NpmAttachment { Base64Data =  "asdd" }  } 
                    }
                }
            };

            var publishPackageCommandHandler = new PublishPackageCommandHandler(logger, service);
            var result = await publishPackageCommandHandler.Handle(input, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
        }
    }
}