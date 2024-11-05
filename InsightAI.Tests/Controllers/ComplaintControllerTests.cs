using InsightAI.API.Controllers;
using InsightAI.Models.Models;
using InsightAI.Repositories.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InsightAI.Tests.Controllers
{
    public class ComplaintControllerTests
    {
        private readonly Mock<IRepository<Complaint>> _mockRepository;
        private readonly ComplaintController _controller;

        public ComplaintControllerTests()
        {
            _mockRepository = new Mock<IRepository<Complaint>>();
            _controller = new ComplaintController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetComplaints_ReturnsOkResult_WithListOfComplaints()
        {
            // Arrange
            var complaints = new List<Complaint>
            {
                new Complaint { Id = 1, CompanyId = 1, Description = "Complaint 1", DateFiled = DateTime.Now },
                new Complaint { Id = 2, CompanyId = 2, Description = "Complaint 2", DateFiled = DateTime.Now }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(complaints);

            // Act
            var result = await _controller.GetComplaints();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedComplaints = Assert.IsAssignableFrom<IEnumerable<Complaint>>(okResult.Value);
            Assert.Equal(2, returnedComplaints.Count());
        }

        [Fact]
        public async Task GetComplaint_ExistingId_ReturnsOkResult_WithComplaint()
        {
            // Arrange
            var complaint = new Complaint { Id = 1, CompanyId = 1, Description = "Complaint 1", DateFiled = DateTime.Now };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(complaint);

            // Act
            var result = await _controller.GetComplaint(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedComplaint = Assert.IsType<Complaint>(okResult.Value);
            Assert.Equal(complaint.Id, returnedComplaint.Id);
        }

        [Fact]
        public async Task GetComplaint_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Complaint)null);

            // Act
            var result = await _controller.GetComplaint(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostComplaint_ValidComplaint_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var complaint = new Complaint { Id = 1, CompanyId = 1, Description = "Complaint 1", DateFiled = DateTime.Now };
            _mockRepository.Setup(repo => repo.AddAsync(complaint)).ReturnsAsync(complaint);

            // Act
            var result = await _controller.PostComplaint(complaint);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedComplaint = Assert.IsType<Complaint>(createdAtActionResult.Value);
            Assert.Equal(complaint.Id, returnedComplaint.Id);
        }

        [Fact]
        public async Task PutComplaint_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var complaint = new Complaint { Id = 1, CompanyId = 1, Description = "Complaint 1", DateFiled = DateTime.Now };
            _mockRepository.Setup(repo => repo.UpdateAsync(complaint)).Returns(Task.CompletedTask);
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(complaint);

            // Act
            var result = await _controller.PutComplaint(1, complaint);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutComplaint_NonMatchingId_ReturnsBadRequest()
        {
            // Arrange
            var complaint = new Complaint { Id = 2, CompanyId = 1, Description = "Complaint 2", DateFiled = DateTime.Now };

            // Act
            var result = await _controller.PutComplaint(1, complaint);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteComplaint_ExistingId_ReturnsNoContent()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteComplaint(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }

}
