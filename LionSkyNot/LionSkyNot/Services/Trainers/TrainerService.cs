using LionSkyNot.Data;
using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Models.Trainers;
using LionSkyNot.Views.ViewModels.Trainers;

namespace LionSkyNot.Services.Trainers
{
    public class TrainerService : ITrainerService
    {

        private LionSkyDbContext data;

        public TrainerService(LionSkyDbContext data)
        {
            this.data = data;
        }

        public void Create(string fullName, int yearsOfExperience, string imageUrl, float height, float weight, DateTime birthDate, int categorieId, string description, string userId)
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
                Description = description,
                UserId = userId
            };

            this.data.Trainers.Add(trainer);

            this.data.SaveChanges();

        }

        public bool Delete(int id)
        {
            var currentTrainer = this.data.Trainers.Where(t => t.Id == id && t.IsDeleted == false)
                                                   .FirstOrDefault();
       
            if(currentTrainer == null)
            {
                return false;
            }

            this.data.Trainers.Remove(currentTrainer);

            this.data.SaveChanges();

            return true;

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
            List<TrainerViewModel> allValidTrainers = new List<TrainerViewModel>();

            var boxer = this.GetTopTrainerByCategorie("Box");
            var mma = this.GetTopTrainerByCategorie("MMA");
            var yoga = this.GetTopTrainerByCategorie("Yoga");
            var fitness = this.GetTopTrainerByCategorie("Fitness");
            var wrestling = this.GetTopTrainerByCategorie("Wrestling");
            var athletic = this.GetTopTrainerByCategorie("Athletic");


            trainers.AddRange(new List<TrainerViewModel>()
            {
                boxer,
                mma,
                yoga,
                fitness,
                wrestling,
                athletic
            });


            foreach (var trainer in trainers)
            {
                if (trainer != null)
                {
                    allValidTrainers.Add(trainer);
                }
            }

            return allValidTrainers;
        }


        public int GetTrainerId(string userId)
        => this.data.Trainers.Where(t => t.UserId == userId).Select(t => t.Id).FirstOrDefault();



        public IEnumerable<TrainerListViewModel> GetAllTrainersByCategory(string category)
        {

            var trainers = this.data.Trainers
                                    .Where(t => t.Categorie.Name == category)
                                    .Select(t => new TrainerListViewModel()
                                    {
                                        FullName = t.FullName,
                                        ImageUrl = t.ImageUrl,
                                        Description = t.Description,
                                        Category = t.Categorie.Name
                                    })
                                    .ToList();

            return trainers;
        }

        public IEnumerable<TrainerUserIdViewModel> GetAllTrainersUserId()
        {

            var trainers = this.data.Trainers
                                    .Select(t => new TrainerUserIdViewModel()
                                    {
                                        UserId = t.UserId
                                    })
                                    .ToList();

            return trainers;
        }


        public IEnumerable<TrainerListViewModel> SearchTrainerByName(string searchedName)
        {

            var trainerQuery = this.data.Trainers.AsQueryable();

            var trainers = trainerQuery
                                        .Where(t => t.FullName.ToLower().Contains(searchedName.ToLower()))
                                        .Select(t => new TrainerListViewModel()
                                        {
                                            FullName = t.FullName,
                                            Description = t.Description,
                                            ImageUrl = t.ImageUrl,
                                        }).ToList();

            return trainers;
        }

        public TrainerViewModel GetTrainerById(int id)
        {

            return this.data.Trainers.Where(t => t.Id == id).Select(t => new TrainerViewModel()
            {
                FullName = t.FullName,
                ImageUrl = t.ImageUrl,
                Description = t.Description,
                YearOfExperience = t.YearOfExperience,
                BirthDate = t.BirthDate,
                Weight = t.Weight,
                Height = t.Height
            }).FirstOrDefault();

        }

        public bool IsTrainer(string userId)
         => this.data.Trainers.Any(t => t.UserId == userId);



        private TrainerViewModel GetTopTrainerByCategorie(string category)
        {

            var trainer = this.data.Trainers
                          .Where(t => t.Categorie.Name == category)
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

            return trainer;
        }


        public IEnumerable<TrainerFormModelForAdmin> GetAllTrainersForAdmin()
        => this.data.Trainers.Where(t => t.IsDeleted == false)
                             .Select(t => new TrainerFormModelForAdmin()
                             {
                                 Id = t.Id,
                                 FullName = t.FullName
                             })
                             .ToList();


    }
}
