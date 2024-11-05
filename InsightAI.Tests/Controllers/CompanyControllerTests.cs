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
    public class CompanyControllerTests
    {
        private readonly Mock<IRepository<Company>> _mockRepository;
        private readonly CompanyController _controller;

        public CompanyControllerTests()
        {
            _mockRepository = new Mock<IRepository<Company>>();
            _controller = new CompanyController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetCompanies_ReturnsOkResult_WithListOfCompanies()
        {
            // Arrange
            var companies = new List<Company>
            {
                new Company { Id = 1, Name = "Company A", Industry = "Tech" },
                new Company { Id = 2, Name = "Company B", Industry = "Finance" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(companies);

            // Act
            var result = await _controller.GetCompanies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCompanies = Assert.IsAssignableFrom<IEnumerable<Company>>(okResult.Value);
            Assert.Equal(2, returnedCompanies.Count());
        }

        [Fact]
        public async Task GetCompany_ExistingId_ReturnsOkResult_WithCompany()
        {
            // Arrange
            var company = new Company { Id = 1, Name = "Company A", Industry = "Tech" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(company);

            // Act
            var result = await _controller.GetCompany(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCompany = Assert.IsType<Company>(okResult.Value);
            Assert.Equal(company.Id, returnedCompany.Id);
        }

        [Fact]
        public async Task GetCompany_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Company)null);

            // Act
            var result = await _controller.GetCompany(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostCompany_ValidCompany_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var company = new Company { Id = 1, Name = "Company A", Industry = "Tech" };
            _mockRepository.Setup(repo => repo.AddAsync(company)).ReturnsAsync(company);

            // Act
            var result = await _controller.PostCompany(company);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedCompany = Assert.IsType<Company>(createdAtActionResult.Value);
            Assert.Equal(company.Id, returnedCompany.Id);
        }

        [Fact]
        public async Task PutCompany_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var company = new Company { Id = 1, Name = "Company A", Industry = "Tech" };
            _mockRepository.Setup(repo => repo.UpdateAsync(company)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCompany(1, company);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutCompany_NonMatchingId_ReturnsBadRequest()
        {
            // Arrange
            var company = new Company { Id = 2, Name = "Company B", Industry = "Finance" };

            // Act
            var result = await _controller.PutCompany(1, company);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteCompany_ExistingId_ReturnsNoContent()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCompany(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }

}
