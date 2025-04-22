using Application.Queries.NpmQueries.DownloadPackageQuery;
using MicroNpmRegistry.Infrastructure.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests
{
    public class DownloadPackageCommandTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_NullRequestForDownloadPackage_ReturnsNull()
        {
            ILogger logger = new Mock<ILogger>().Object;
            IFileService service = new Mock<IFileService>().Object;

            var downloadPackageQueryHandler = new DownloadPackageQueryHandler(service);

            var result = await downloadPackageQueryHandler.Handle(null, CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_MissingPackageRequestForDownloadPackage_ReturnsEmptyPath()
        {
            ILogger logger = new Mock<ILogger>().Object;
            var serviceMock = new Mock<IFileService>();

            serviceMock.Setup(m => m.Exists(It.IsAny<string>())).Returns(false);

            var service = serviceMock.Object;

            var downloadPackageQueryHandler = new DownloadPackageQueryHandler(service);

            var result = await downloadPackageQueryHandler.Handle(new DownloadPackageCommand
            {
                FileName = "test",
                LocalStoragePath = "test"
            }, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PackageFilePath, Is.Null);
        }
    }
}
