﻿using LionSkyNot.Data;
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

        public void Create(string fullName, int yearsOfExperience, string imageUrl, float height, float weight, DateTime birthDate, int categorieId, string description)
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

            var boxer = this.GetTopTrainerByCategorie("Box");
            var mma = this.GetTopTrainerByCategorie("MMA");
            var yoga = this.GetTopTrainerByCategorie("Yoga");
            var fitness = this.GetTopTrainerByCategorie("Fitness");
            var wrestling = this.GetTopTrainerByCategorie("Wrestling");
            var athletic = this.GetTopTrainerByCategorie("Athletic");

            trainers.Add(boxer);
            trainers.Add(mma);
            trainers.Add(yoga);
            trainers.Add(fitness);
            trainers.Add(wrestling);
            trainers.Add(athletic);

            return trainers;
        }


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

    }
}
