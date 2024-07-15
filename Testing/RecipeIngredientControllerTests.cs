using Application.Contracts.RecipeIngredientContracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Recipe.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Testing
{
    public class RecipeIngredientControllerTests
    {
        private readonly Mock<IRecipeIngredientApplication> _mockIngredients;
        private readonly RecipeIngredientController _controller;

        public RecipeIngredientControllerTests()
        {
            _mockIngredients = new Mock<IRecipeIngredientApplication>();
            _controller = new RecipeIngredientController(_mockIngredients.Object);
        }

        [Fact]
        public void GetAll_ReturnsOk_WhenResultIsNotNull()
        {
            // Arrange
            int recipeId = 1;
            _mockIngredients.Setup(x => x.SelectAllIngredient(recipeId)).Returns(new List<IngredientViewModel>());

            // Act
            var result = _controller.GetAll(recipeId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetIngredient_ReturnsBadRequest_WhenResultIsNull()
        {
            // Arrange
            int ingredientId = 1;
            _mockIngredients.Setup(x => x.FindIngredient(r => r.Id == ingredientId)).Returns((IngredientViewModel)null);

            // Act
            var result = _controller.GetIngredient(ingredientId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void RemoveIngredient_ReturnsOk_WhenCalled()
        {
            // Arrange
            int ingredientId = 1;

            // Act
            var result = _controller.RemoveIngredient(ingredientId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
