using Application.Contracts.UserContracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Recipe.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Testing
{
    public class SignUpModelTests
    {
        private readonly Mock<IUserApplication> _mockUserApplication;
        private readonly SignUpModel _signUpModel;

        public SignUpModelTests()
        {
            _mockUserApplication = new Mock<IUserApplication>();
            _signUpModel = new SignUpModel(_mockUserApplication.Object);
        }

        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _signUpModel.ModelState.AddModelError("Error", "Something went wrong...");

            // Act
            var result = await _signUpModel.OnPostAsync(new CreateUserCommand());

            // Assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenRegistrationFails()
        {
            // Arrange
            int? id = 1;
            _mockUserApplication.Setup(x => x.AddUser(It.IsAny<CreateUserCommand>(), out id)).Returns(false);

            // Act
            var result = await _signUpModel.OnPostAsync(new CreateUserCommand());

            // Assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_AddsModelError_WhenRegistrationFails()
        {
            // Arrange
            int? id = 1;
            _mockUserApplication.Setup(x => x.AddUser(It.IsAny<CreateUserCommand>(), out id)).Returns(false);

            // Act
            await _signUpModel.OnPostAsync(new CreateUserCommand());

            // Assert
            Assert.True(_signUpModel.ModelState.ErrorCount > 0);
        }
    }

}
