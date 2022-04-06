using LionSkyNot.Data;

using LionSkyNot.Data.Models.Recipe;

using LionSkyNot.Models.Recipe;

using LionSkyNot.Models.Recipes;

using LionSkyNot.Views.ViewModels.Recipes;


namespace LionSkyNot.Services.Recipes
{
    public class RecipeService : IRecipeService
    {

        private LionSkyDbContext data;


        public RecipeService(LionSkyDbContext data)
        {
            this.data = data;
        }



        public void Create(
                           string name,
                           string description,
                           string imgUrl)
        {

            var recipe = new Recipe()
            {
                Name = name,
                Description = description,
                ImageUrl = imgUrl
            };

            this.data.Recipes.Add(recipe);

            this.data.SaveChanges();

        }


        public bool Delete(int id)
        {
            var currentRecipe = this.data.Recipes
                                         .Where(r => r.Id == id && r.IsDeleted == false)
                                         .FirstOrDefault();

            if (currentRecipe == null)
            {
                return false;
            }

            currentRecipe.IsDeleted = true;

            this.data.SaveChanges();

            return true;
        }


        public bool EditRecipe(
                               int id,
                               string name,
                               string imageUrl,
                               string description)
        {
            var currentRecipe = this.data.Recipes
                                         .Where(r => r.Id == id && r.IsDeleted == false)
                                         .FirstOrDefault();

            if (currentRecipe == null)
            {
                return false;
            }

            currentRecipe.Name = name;
            currentRecipe.Description = description;
            currentRecipe.ImageUrl = imageUrl;


            this.data.SaveChanges();

            return true;
        }


        public IEnumerable<RecipeViewModel> GetAll()
        => data.Recipes
               .Where(x => x.IsDeleted == false)
               .Select(x => new RecipeViewModel()
               {
                   Name = x.Name,
                   Description = x.Description,
                   ImageUrl = x.ImageUrl,
               })
               .ToList();


        public RecipeFormModel GetRecipeById(int id)
         => this.data.Recipes
                      .Where(r => r.Id == id && r.IsDeleted == false)
                      .Select(r => new RecipeFormModel()
                      {
                          Name = r.Name,
                          ImageUrl = r.ImageUrl,
                          Description = r.Description,
                      })
                      .FirstOrDefault();


        public IEnumerable<RecipeFormModelForAdmin> GetAllRecipesForAdmin()
        => this.data.Recipes
                    .Where(r => r.IsDeleted == false)
                    .Select(r => new RecipeFormModelForAdmin()
                    {
                        Id = r.Id,
                        Name = r.Name
                    })
                    .ToList();

    }
}
