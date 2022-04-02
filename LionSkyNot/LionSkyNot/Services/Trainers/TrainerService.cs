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

        public void Create(
                           string fullName,
                           int yearsOfExperience,
                           string imageUrl,
                           float height,
                           float weight,
                           DateTime birthDate,
                           int categorieId,
                           string description,
                           string userId,
                           bool isCandidate)
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
                UserId = userId,
                IsCandidate = isCandidate
            };

            this.data.Trainers.Add(trainer);

            this.data.SaveChanges();

        }

        public void AddCandidate(Trainer currentCandidate)
        {

            currentCandidate.IsCandidate = false;

            this.data.SaveChanges();
        }

        public bool Delete(int id)
        {
            var currentTrainer = this.data.Trainers.Where(t => t.Id == id)
                                                   .FirstOrDefault();

            if (currentTrainer == null)
            {
                return false;
            }

            this.data.Trainers.Remove(currentTrainer);

            this.data.SaveChanges();

            return true;

        }

        public bool IsTrainer(string userId)
         => this.data.Trainers
                     .Any(t => t.UserId == userId && t.IsCandidate == false);


        public bool IsCandidate(string userId)
        => this.data.Trainers
                    .Any(t => t.UserId == userId && t.IsCandidate == true);


        public Trainer GetCandidateTrainerById(int id)
         => this.data.Trainers
                     .Where(t => t.Id == id && t.IsCandidate == true)
                     .FirstOrDefault();


        public IEnumerable<CategorieViewModel> GetAllCategories()
        => this.data.Categories
                    .Select(c => new CategorieViewModel
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
        => this.data.Trainers
                    .Where(t => t.UserId == userId && t.IsCandidate == false)
                    .Select(t => t.Id)
                    .FirstOrDefault();



        public IEnumerable<TrainerListViewModel> GetAllTrainersByCategory(string category)
        {

            var trainers = this.data.Trainers
                                    .Where(t => t.Categorie.Name == category && t.IsCandidate == false)
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

        public IEnumerable<TrainerUserIdViewModel> GetAllCandidateTrainersUserId()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<TrainerUserIdViewModel> GetAllTrainersUserId(bool isCandidate)
        {

            var trainers = this.data.Trainers
                                    .Where(t => t.IsCandidate == isCandidate)
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

        public IEnumerable<TrainerCandidateViewModel> SortedByYearOfExperience()
        => this.data.Trainers
                    .Where(t => t.IsCandidate == true)
                    .OrderByDescending(t => t.YearOfExperience)
                    .Select(t => new TrainerCandidateViewModel()
                    {
                        Id = t.Id,
                        FullName = t.FullName,
                        ImageUrl = t.ImageUrl,
                        YearOfExperience = t.YearOfExperience,
                        BirthDate = t.BirthDate,
                        Category = t.Categorie.Name
                    })
                    .ToList();


        public IEnumerable<TrainerCandidateViewModel> SortedByAge()
        => this.data.Trainers
                    .Where(t => t.IsCandidate == true)
                    .OrderByDescending(t => t.BirthDate.Year)
                    .Select(t => new TrainerCandidateViewModel()
                    {
                        Id = t.Id,
                        FullName = t.FullName,
                        ImageUrl = t.ImageUrl,
                        YearOfExperience = t.YearOfExperience,
                        BirthDate = t.BirthDate,
                        Category = t.Categorie.Name
                    })
                    .ToList();


        public TrainerViewModel GetTrainerById(int id)
        {

            return this.data.Trainers
                            .Where(t => t.Id == id && t.IsCandidate == false)
                            .Select(t => new TrainerViewModel()
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



        private TrainerViewModel GetTopTrainerByCategorie(string category)
        {

            var trainer = this.data.Trainers
                          .Where(t => t.Categorie.Name == category && t.IsCandidate == false)
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
        => this.data.Trainers
                    .Where(t => t.IsCandidate == false)
                    .Select(t => new TrainerFormModelForAdmin()
                    {
                        Id = t.Id,
                        FullName = t.FullName
                    })
                    .ToList();


        public IEnumerable<TrainerCandidateViewModel> GetAllTrainerCandidates()
        => this.data.Trainers
                    .Where(t => t.IsCandidate == true)
                    .Select(t => new TrainerCandidateViewModel()
                    {
                        Id = t.Id,
                        FullName = t.FullName,
                        BirthDate = t.BirthDate,
                        Category = t.Categorie.Name,
                        ImageUrl = t.ImageUrl,
                        YearOfExperience = t.YearOfExperience
                    })
                    .ToList();

    }
}