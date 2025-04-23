using Application.Queries.NpmQueries.DownloadPackageQuery;
using Application.Queries.NpmQueries.GetPackageMetatDataQuery;
using MicroNpmRegistry.Domain.Entities;
using MicroNpmRegistry.Infrastructure.Storage;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests
{
    public class GetMetaDataQueryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public  async Task Test_NullInputToGetMetaData_ReturnsNull()
        {
            var logger = new Mock<ILogger<GetPackageMetaDataQueryHandler>>().Object;
            IFileService service = new Mock<IFileService>().Object;

            var getMetatDataQueryHandler = new GetPackageMetaDataQueryHandler(logger, service);

            var result = await getMetatDataQueryHandler.Handle(null, CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_EmptyInputToGetMetaData_ReturnsNull()
        {
            var logger = new Mock<ILogger<GetPackageMetaDataQueryHandler>>().Object;
            IFileService service = new Mock<IFileService>().Object;

            var getMetatDataQueryHandler = new GetPackageMetaDataQueryHandler(logger, service);

            var result = await getMetatDataQueryHandler.Handle(new GetPackageMetaDataQueryRequest(), CancellationToken.None);

            Assert.That(result, Is.Null);
        }


        [Test]
        public async Task Test_MissingFileInputToGetMetaData_ReturnsNull()
        {
            var logger = new Mock<ILogger<GetPackageMetaDataQueryHandler>>().Object;
            var serviceMock = new Mock<IFileService>();
            serviceMock.Setup(m => m.Exists(It.IsAny<string>())).Returns(false);
            var service  = serviceMock.Object;

            var getMetatDataQueryHandler = new GetPackageMetaDataQueryHandler(logger, service);

            var result = await getMetatDataQueryHandler.Handle(new GetPackageMetaDataQueryRequest
            {
                FileName = "test",
                StorageConfiguration = new StorageConfiguration()
            }, CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_InvalidJsonMetaFileInputToGetMetaData_ReturnsNull()
        {
            var logger = new Mock<ILogger<GetPackageMetaDataQueryHandler>>().Object;
            var serviceMock = new Mock<IFileService>();
            serviceMock.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
            serviceMock.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns("{");
            var service = serviceMock.Object;

            var getMetatDataQueryHandler = new GetPackageMetaDataQueryHandler(logger, service);

            var result = await getMetatDataQueryHandler.Handle(new GetPackageMetaDataQueryRequest
            {
                FileName = "test",
                StorageConfiguration = new StorageConfiguration()
            }, CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_EmptyJsonMetaFileInputToGetMetaData_ReturnsNull()
        {
            var logger = new Mock<ILogger<GetPackageMetaDataQueryHandler>>().Object;
            var serviceMock = new Mock<IFileService>();
            serviceMock.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
            serviceMock.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns("{}");
            var service = serviceMock.Object;

            var getMetatDataQueryHandler = new GetPackageMetaDataQueryHandler(logger, service);

            var result = await getMetatDataQueryHandler.Handle(new GetPackageMetaDataQueryRequest
            {
                FileName = "test",
                StorageConfiguration = new StorageConfiguration()
            }, CancellationToken.None);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_ValidMetaFileInputToGetMetaData_ReturnsNull()
        {
            var logger = new Mock<ILogger<GetPackageMetaDataQueryHandler>>().Object;
            var serviceMock = new Mock<IFileService>();
            serviceMock.Setup(m => m.Exists(It.IsAny<string>())).Returns(true);
            serviceMock.Setup(m => m.ReadAllText(It.IsAny<string>())).Returns(@"{ Id : ""1223"", Name : ""Test"" } ");
            var service = serviceMock.Object;

            var getMetatDataQueryHandler = new GetPackageMetaDataQueryHandler(logger, service);

            var result = await getMetatDataQueryHandler.Handle(new GetPackageMetaDataQueryRequest
            {
                FileName = "test",
                StorageConfiguration = new StorageConfiguration()
            }, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
        }
    }
}
