using LionSkyNot.Models.Class;
using LionSkyNot.Views.ViewModels.Classes;

namespace LionSkyNot.Services.Classes
{
    public interface IClassService
    {

        IEnumerable<ClassFormModelForAdmin> GetAllClassesForAdmin();

        IEnumerable<ClassViewModel> GetAllClasses();

        IEnumerable<TrainerClassViewModel> GetAllTrainers();

        IEnumerable<ClassTrainerViewModel> GetAllTrainerClasses(int trainerId);

        void Create(string fullname,
            string imageUrl,
            decimal price,
            int MaxPractitionerCount,
            int trainerId,
            DateTime startDateTime,
            DateTime endDateTime);

    }
}
