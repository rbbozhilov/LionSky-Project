using System.Collections.Generic;

using LionSkyNot.Controllers;

using LionSkyNot.Data.Models.Recipe;

using LionSkyNot.Services.Recipes;

using LionSkyNot.Tests.Mock;

using LionSkyNot.Views.ViewModels.Recipes;

using Microsoft.AspNetCore.Mvc;

using Xunit;


namespace LionSkyNot.Tests.Controllers
{
    public class RecipeControllerTest
    {

        [Fact]
        public void RecipeIndex_ShouldReturnCorrectViewModelAndViewResult()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var recipeController = new RecipeController(recipeService);
            
            var recipe = new Recipe()
            {
                Id = 1,
                Description = "description",
                ImageUrl = "imageurl",
                Name = "pizza margarita"
            };

            data.Recipes.Add(recipe);
            data.SaveChanges();

            //Act

            var currentRecipes = new List<RecipeViewModel>();

            var viewResult = recipeController.Index(currentRecipes);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(viewResult);

            var listOfRecipes = Assert.IsType<List<RecipeViewModel>>(viewModel.Model);

            Assert.Equal(1, listOfRecipes.Count);
   
        }

    }
}
