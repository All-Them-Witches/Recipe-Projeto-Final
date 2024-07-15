using Moq;
using Xunit;
using Recipe.Api;
using Application.Contracts.UserContracts;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System.Linq.Expressions;
using Recipe.Api.Dto;

namespace Testing
{
    public class UserControllerTests
    {
        private readonly Mock<IUserApplication> _mockUserApplication;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserApplication = new Mock<IUserApplication>();
            _controller = new UserController(_mockUserApplication.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WhenUsersExist()
        {
            // Arrange
            _mockUserApplication.Setup(app => app.SelectAllUsers()).Returns(new List<UserViewModel>());

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Get_ReturnsNotFoundResult_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserApplication.Setup(app => app.FindUser(It.IsAny<Expression<Func<User, bool>>>())).Returns((UserViewModel)null);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(result);
        }
        [Fact]
        public void Post_ReturnsBadRequestResult_WhenAddUserFails()
        {
            // Arrange
            int? userId = null;
            _mockUserApplication.Setup(app => app.AddUser(It.IsAny<CreateUserCommand>(), out userId)).Returns(false);

            // Act
            var result = _controller.Post("username", "email", "password");

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(result);
        }
        [Fact]
        public void Get_ReturnsOkResult_WhenUserExists()
        {
            // Arrange
            _mockUserApplication.Setup(app => app.FindUser(It.IsAny<Expression<Func<User, bool>>>())).Returns(new UserViewModel());

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Put_ReturnsOkResult_WhenUpdateSucceeds()
        {
            // Arrange
            _mockUserApplication.Setup(app => app.Update(It.IsAny<UpdateUserCommand>())).Returns(true);

            // Act
            var result = _controller.Put(new UserDto());

            // Assert
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public void Delete_ReturnsOkResult_WhenDeActiveUserSucceeds()
        {
            // Arrange
            _mockUserApplication.Setup(app => app.DeActiveUser(It.IsAny<int>())).Returns(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public void Delete_ReturnsNotFoundResult_WhenDeActiveUserFails()
        {
            // Arrange
            _mockUserApplication.Setup(app => app.DeActiveUser(It.IsAny<int>())).Returns(false);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
