using InsightAI.API.Controllers;
using InsightAI.Models.Models;
using InsightAI.Services.Interfaces;
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
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UserController(_mockUserService.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "User1", Email = "user1@example.com", PasswordHash = "hash1" },
                new User { Id = 2, Username = "User2", Email = "user2@example.com", PasswordHash = "hash2" }
            };

            _mockUserService.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Equal(2, returnedUsers.Count());
        }

        [Fact]
        public async Task GetUser_ExistingId_ReturnsOkResult_WithUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", Email = "user1@example.com", PasswordHash = "hash1" };
            _mockUserService.Setup(service => service.GetAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetUser(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(user.Id, returnedUser.Id);
        }

        [Fact]
        public async Task GetUser_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetAsync(1)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.GetUser(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostUser_ValidUser_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", Email = "user1@example.com", PasswordHash = "hash1" };
            _mockUserService.Setup(service => service.AddAsync(user)).ReturnsAsync(user);

            // Act
            var result = await _controller.PostUser(user);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedUser = Assert.IsType<User>(createdAtActionResult.Value);
            Assert.Equal(user.Id, returnedUser.Id);
        }

        [Fact]
        public async Task PutUser_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", Email = "user1@example.com", PasswordHash = "hash1" };
            _mockUserService.Setup(service => service.UpdateAsync(user)).Returns(Task.CompletedTask);
            _mockUserService.Setup(service => service.GetAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.PutUser(1, user);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutUser_NonMatchingId_ReturnsBadRequest()
        {
            // Arrange
            var user = new User { Id = 2, Username = "User2", Email = "user2@example.com", PasswordHash = "hash2" };

            // Act
            var result = await _controller.PutUser(1, user);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ExistingId_ReturnsNoContent()
        {
            // Arrange
            _mockUserService.Setup(service => service.DeleteAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
