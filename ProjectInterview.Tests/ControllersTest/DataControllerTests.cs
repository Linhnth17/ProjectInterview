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
using Xunit;

namespace ProjectInterview.Tests
{
    public class DataControllerTests
    {
        private readonly Mock<ICsvService> _mockCsvService;
        private readonly Mock<ILogger<DataController>> _mockLogger;
        private readonly DataController _controller;

        public DataControllerTests()
        {
            _mockCsvService = new Mock<ICsvService>();
            _mockLogger = new Mock<ILogger<DataController>>();
            _controller = new DataController(_mockCsvService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("~/Views/Data/Index.cshtml", viewResult.ViewName);
        }

        [Fact]
        public void UploadCsv_ReturnsViewResult_WhenFileIsNull()
        {
            // Act
            var result = _controller.UploadCsv1(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", viewResult.ViewName);
            Assert.Equal("Please upload a valid CSV file.", viewResult.ViewData["ErrorMessage"]);
        }

        [Fact]
        public void UploadCsv_ReturnsViewResult_WhenInvalidFileType()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("invalid.txt");
            fileMock.Setup(f => f.Length).Returns(1);

            // Act
            var result = _controller.UploadCsv1(fileMock.Object);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", viewResult.ViewName);
            Assert.Equal("Please upload a valid CSV file.", viewResult.ViewData["ErrorMessage"]);
        }
    }
}
