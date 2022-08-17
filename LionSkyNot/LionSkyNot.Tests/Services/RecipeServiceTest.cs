using System.Linq;
using System.Threading.Tasks;
using LionSkyNot.Services.Recipes;

using LionSkyNot.Tests.Mock;

using Xunit;


namespace LionSkyNot.Tests.Services
{
    public class RecipeServiceTest
    {

        [Fact]
        public async Task CreateRecipe_ShouldBeCorrect()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var name = "pizza";
            var description = "best pizza ever";
            var imgUrl = "Some image";


            //Act
            await recipeService.CreateAsync(name, description, imgUrl);


            //Assert
            Assert.True(data.Recipes.Any(r => r.Name == name));


        }


        [Fact]
        public async Task EditRecipe_ShouldReturnFalse()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);

            //Assert
            Assert.False(await recipeService.EditRecipeAsync(1, "name", "imageurl", "description"));

        }


        [Fact]
        public async Task EditRecipe_ShouldBeCorrect()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var name = "pizza";
            var description = "best pizza ever";
            var imgUrl = "Some image";
            var name1 = "pizza1";
            var description1 = "best1 pizza ever";
            var imgUrl1 = "Some1 image";


            //Act
            await recipeService.CreateAsync(name, description, imgUrl);
            var recipe = data.Recipes.FirstOrDefault(r => r.Name == name);
            var result = await recipeService.EditRecipeAsync(1, name1, imgUrl1, description1);

            //Assert
            Assert.Equal(name1, recipe.Name);
            Assert.Equal(description1, recipe.Description);
            Assert.Equal(imgUrl1, recipe.ImageUrl);
            Assert.True(result);


        }


        [Fact]
        public async Task GetAllRecipes_ShouldBeCorrect()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var name = "pizza";
            var description = "best pizza ever";
            var imgUrl = "Some image";
            var name1 = "pizza1";
            var description1 = "best1 pizza ever";
            var imgUrl1 = "Some1 image";


            //Act
            await recipeService.CreateAsync(name, description, imgUrl);
            await recipeService.CreateAsync(name1, description1, imgUrl1);
            var allRecipes = recipeService.GetAll();

            //Assert
            Assert.Equal(2, allRecipes.Count());
            Assert.Equal(name, allRecipes.First().Name);
            Assert.Equal(name1, allRecipes.Last().Name);

        }


        [Fact]
        public void GetRecipeById_ShouldReturnNull()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);

            //Assert
            Assert.Null(recipeService.GetRecipeById(1));

        }


        [Fact]
        public async Task GetRecipeById_ShouldBeCorrect()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var name = "pizza";
            var description = "best pizza ever";
            var imgUrl = "Some image";

            //Act
            await recipeService.CreateAsync(name, description, imgUrl);
            var recipe = recipeService.GetRecipeById(1);

            //Assert
            Assert.NotNull(recipe);
            Assert.Equal(name, recipe.Name);

        }


        [Fact]
        public async Task GetAllRecipes_ForAdmin_ShouldBeCorrect()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var name = "pizza";
            var description = "best pizza ever";
            var imgUrl = "Some image";
            var name1 = "pizza1";
            var description1 = "best1 pizza ever";
            var imgUrl1 = "Some1 image";


            //Act
            await recipeService.CreateAsync(name, description, imgUrl);
            await recipeService.CreateAsync(name1, description1, imgUrl1);
            var allRecipes = recipeService.GetAllRecipesForAdmin();

            //Assert
            Assert.Equal(2, allRecipes.Count());
            Assert.Equal(name, allRecipes.First().Name);
            Assert.Equal(name1, allRecipes.Last().Name);

        }


        [Fact]
        public async Task DeleteRecipe_ShouldReturnFalse()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);

            //Assert
            Assert.False(await recipeService.DeleteAsync(1));

        }


        [Fact]
        public async Task DeleteRecipe_ShouldBeCorrectAndReturnTrue()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var recipeService = new RecipeService(data);
            var name = "pizza";
            var description = "best pizza ever";
            var imgUrl = "Some image";


            //Act
            await recipeService.CreateAsync(name, description, imgUrl);
            var result = await recipeService.DeleteAsync(1);

            //Assert
            Assert.True(result);
            Assert.True(data.Recipes.FirstOrDefault(r => r.Name == name).IsDeleted);


        }

    }
}
