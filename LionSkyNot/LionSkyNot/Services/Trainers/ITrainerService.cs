using LionSkyNot.Views.ViewModels.Trainers;

namespace LionSkyNot.Services.Trainers
{
    public interface ITrainerService
    {

        void Create(string fullName, int yearsOfExperience, string imageUrl, float height, float weight, DateTime birthDate , int categorieId , string description);

        IEnumerable<CategorieViewModel> GetAllCategories();

        IEnumerable<TrainerViewModel> TopTrainers();


    }
}
