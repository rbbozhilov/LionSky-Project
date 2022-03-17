﻿using LionSkyNot.Views.ViewModels.Trainers;

namespace LionSkyNot.Services.Trainers
{
    public interface ITrainerService
    {

        void Create(string fullName, int yearsOfExperience, string imageUrl, float height, float weight, DateTime birthDate, int categorieId, string description);


        TrainerViewModel GetTrainerById(int id);

        IEnumerable<CategorieViewModel> GetAllCategories();

        IEnumerable<TrainerViewModel> TopTrainers();

        IEnumerable<TrainerListViewModel> GetAllTrainersByCategory(string category);

        IEnumerable<TrainerListViewModel> SearchTrainerByName(string searchedName);

    }
}
