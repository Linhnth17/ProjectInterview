using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectInterview.Controllers;
using ProjectInterview.Models;
using ProjectInterview.Services;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjectInterview.Tests
{
    public class DataControllerTests
    {
        private readonly Mock<ICsvService> _mockCsvService;
        private readonly Mock<ILogger<DataController>> _mockLogger;
        private readonly DataController _controller;
        private readonly string testFilePath = "TestData/TestData.csv";

        public DataControllerTests()
        {
            _mockCsvService = new Mock<ICsvService>();
            _mockLogger = new Mock<ILogger<DataController>>();
            _controller = new DataController(_mockCsvService.Object, _mockLogger.Object);
        }

        private IFormFile CreateMockCsvFileFromDisk()
        {
            if (!File.Exists(testFilePath))
            {
                throw new FileNotFoundException($"Test file not found: {testFilePath}");
            }

            byte[] fileBytes = File.ReadAllBytes(testFilePath);
            var stream = new MemoryStream(fileBytes);
            return new FormFile(stream, 0, fileBytes.Length, "file", Path.GetFileName(testFilePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/csv"
            };
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("~/Views/Data/Index.cshtml", viewResult.ViewName);
        }

        [Fact]
        public async Task UploadCsv1_ReturnsEmptyList_WhenFileIsNull()
        {
            var result = await _controller.UploadCsv1(null);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UploadCsv1_ReturnsEmptyList_WhenInvalidFileType()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("invalid.txt");
            fileMock.Setup(f => f.Length).Returns(1);

            var result = await _controller.UploadCsv1(fileMock.Object);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UploadCsv1_DateWithoutTime_DefaultsToMidnight()
        {
            var mockFile = CreateMockCsvFileFromDisk();
            var mockData = new List<DataEntry>
            {
                new DataEntry { Date = "2024-03-09", MarketPrice = 0 }
            };

            _mockCsvService.Setup(s => s.ParseCsvFileAsync(It.IsAny<Stream>()))
                           .ReturnsAsync(mockData);

            var result = await _controller.UploadCsv1(mockFile);

            Assert.Single(result);
            Assert.Equal("2024-03-09 00:00", result[0].Date);
        }
    }
}
