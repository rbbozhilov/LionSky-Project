using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Models.Trainers;
using LionSkyNot.Views.ViewModels.Trainers;

namespace LionSkyNot.Services.Trainers
{
    public interface ITrainerService
    {

        void Create(
                    string fullName,
                    int yearsOfExperience,
                    string imageUrl,
                    float height,
                    float weight,
                    DateTime birthDate,
                    int categorieId,
                    string description,
                    string userId,
                    bool isCandidate);

        void AddCandidate(Trainer trainer);

       

        bool IsTrainer(string userId);

        bool IsCandidate(string userId);

        int GetTrainerId(string userId);

        bool Delete(int id);

        TrainerViewModel GetTrainerById(int id);

        Trainer GetCandidateTrainerById(int id);

        IEnumerable<TrainerFormModelForAdmin> GetAllTrainersForAdmin();

        IEnumerable<TrainerCandidateViewModel> GetAllTrainerCandidates();

        IEnumerable<CategorieViewModel> GetAllCategories();

        IEnumerable<TrainerViewModel> TopTrainers();

        IEnumerable<TrainerListViewModel> GetAllTrainersByCategory(string category);

        IEnumerable<TrainerUserIdViewModel> GetAllTrainersUserId(bool isCandidate);

        IEnumerable<TrainerListViewModel> SearchTrainerByName(string searchedName);

        IEnumerable<TrainerCandidateViewModel> SortedByYearOfExperience();

        IEnumerable<TrainerCandidateViewModel> SortedByAge();


        


    }
}
