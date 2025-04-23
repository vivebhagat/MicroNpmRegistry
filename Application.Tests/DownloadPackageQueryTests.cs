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
    public class DownloadPackageQueryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_NullRequestForDownloadPackage_ReturnsNull()
        {
            var logger = new Mock<ILogger<DownloadPackageQueryHandler>>().Object;
            IFileService service = new Mock<IFileService>().Object;

            var downloadPackageQueryHandler = new DownloadPackageQueryHandler(logger,service);

            var result = await downloadPackageQueryHandler.Handle(null, CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_MissingPackageRequestForDownloadPackage_ReturnsEmptyPath()
        {
            var logger = new Mock<ILogger<DownloadPackageQueryHandler>>().Object;
            var serviceMock = new Mock<IFileService>();

            serviceMock.Setup(m => m.Exists(It.IsAny<string>())).Returns(false);

            var service = serviceMock.Object;

            var downloadPackageQueryHandler = new DownloadPackageQueryHandler(logger, service);

            var result = await downloadPackageQueryHandler.Handle(new DownloadPackageQueryRequest
            {
                FileName = "test",
                LocalStoragePath = "test"
            }, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PackageFilePath, Is.Null);
        }
    }
}
