using LionSkyNot.Data;
using LionSkyNot.Views.ViewModels.Trainers;
using LionSkyNot.Data.Models.Class;

namespace LionSkyNot.Services.Trainers
{
    public class TrainerService : ITrainerService
    {

        private LionSkyDbContext data;

        public TrainerService(LionSkyDbContext data)
        {
            this.data = data;
        }

        public void Create(string fullName, int yearsOfExperience, string imageUrl, float height, float weight, DateTime birthDate, int categorieId , string description)
        {
            var trainer = new Trainer()
            {
                FullName = fullName,
                ImageUrl = imageUrl,
                YearOfExperience = yearsOfExperience,
                Height = height,
                Weight = weight,
                BirthDate = birthDate,
                CategorieId = categorieId,
                Description = description
            };

            this.data.Trainers.Add(trainer);

            this.data.SaveChanges();

        }

        public IEnumerable<CategorieViewModel> GetAllCategories()
        => this.data.Categories.Select(c => new CategorieViewModel
        {
            Id = c.Id,
            Name = c.Name
        });


        public IEnumerable<TrainerViewModel> TopTrainers()
        {

            List<TrainerViewModel> trainers = new List<TrainerViewModel>();

            var boxer = this.data.Trainers
                          .Where(t => t.Categorie.Name == "Box")
                          .Select(t => new TrainerViewModel()
                          {
                              FullName = t.FullName,
                              ImageUrl = t.ImageUrl,
                              Description = t.Description,
                              YearOfExperience = t.YearOfExperience,
                              BirthDate = t.BirthDate,
                              Weight = t.Weight,
                              Height = t.Height
                          })
                          .OrderByDescending(t => t.YearOfExperience)
                          .Take(1)
                          .FirstOrDefault();

            var mma = this.data.Trainers
                         .Where(t => t.Categorie.Name == "MMA")
                         .Select(t => new TrainerViewModel()
                         {
                             FullName = t.FullName,
                             ImageUrl = t.ImageUrl,
                             Description = t.Description,
                             YearOfExperience = t.YearOfExperience,
                             BirthDate = t.BirthDate,
                             Weight = t.Weight,
                             Height = t.Height
                         })
                         .OrderByDescending(t => t.YearOfExperience)
                         .Take(1)
                         .FirstOrDefault();

            var yoga = this.data.Trainers
                         .Where(t => t.Categorie.Name == "Yoga")
                         .Select(t => new TrainerViewModel()
                         {
                             FullName = t.FullName,
                             ImageUrl = t.ImageUrl,
                             Description = t.Description,
                             YearOfExperience = t.YearOfExperience,
                             BirthDate = t.BirthDate,
                             Weight = t.Weight,
                             Height = t.Height
                         })
                         .OrderByDescending(t => t.YearOfExperience)
                         .Take(1)
                         .FirstOrDefault();

            var fitness = this.data.Trainers
                         .Where(t => t.Categorie.Name == "Fitness")
                         .Select(t => new TrainerViewModel()
                         {
                             FullName = t.FullName,
                             ImageUrl = t.ImageUrl,
                             Description = t.Description,
                             YearOfExperience = t.YearOfExperience,
                             BirthDate = t.BirthDate,
                             Weight = t.Weight,
                             Height = t.Height
                         })
                         .OrderByDescending(t => t.YearOfExperience)
                         .Take(1)
                         .FirstOrDefault();

            trainers.Add(boxer);
            trainers.Add(mma);
            trainers.Add(yoga);
            trainers.Add(fitness);

            return trainers;
        }



    }
}
