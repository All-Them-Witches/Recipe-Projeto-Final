using Application.Contracts.RecipeContracts;
using Application.Contracts.UserContracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Recipe.Api;
using System.Linq.Expressions;
using Xunit;

namespace Testing
{
    public class RecipeControllerTests
    {
        private readonly Mock<IRecipeApplication> _mockRecipeApplication;
        private readonly RecipeController _controller;

        public RecipeControllerTests()
        {
            _mockRecipeApplication = new Mock<IRecipeApplication>();
            _controller = new RecipeController(_mockRecipeApplication.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WhenRecipesExist()
        {
            // Arrange
            _mockRecipeApplication.Setup(app => app.SelectAllRecipes()).Returns(new List<RecipeViewModel>());

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_ReturnsNotFoundResult_WhenRecipeDoesNotExist()
        {
            // Arrange
            _mockRecipeApplication.Setup(app => app.FindRecipe(It.IsAny<Expression<Func<Domain.Entities.Recipe, bool>>>())).Returns((RecipeViewModel)null);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_ReturnsBadRequestResult_WhenAddRecipeFails()
        {
            // Arrange
            int? recipeId = null;
            _mockRecipeApplication.Setup(app => app.AddRecipe(It.IsAny<CreateRecipeCommand>(), out recipeId)).Returns(false);

            // Act
            var result = _controller.Post(new CreateRecipeCommand());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Put_ReturnsOkResult_WhenUpdateSucceeds()
        {
            // Arrange
            _mockRecipeApplication.Setup(app => app.Update(It.IsAny<UpdateRecipeCommand>())).Returns(true);

            // Act
            var result = _controller.Put(new UpdateRecipeCommand());

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Delete_ReturnsOkResult_WhenDeleteSucceeds()
        {
            // Arrange
            _mockRecipeApplication.Setup(app => app.DeleteRecipe(It.IsAny<int>())).Returns(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNotFoundResult_WhenDeleteFails()
        {
            // Arrange
            _mockRecipeApplication.Setup(app => app.DeleteRecipe(It.IsAny<int>())).Returns(false);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }

}
