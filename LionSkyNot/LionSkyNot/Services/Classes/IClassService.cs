using LionSkyNot.Models.Class;
using LionSkyNot.Views.ViewModels.Classes;

namespace LionSkyNot.Services.Classes
{
    public interface IClassService
    {

        void Create(
                    string fullname,
                    string imageUrl,
                    decimal price,
                    int MaxPractitionerCount,
                    int trainerId,
                    DateTime startDateTime,
                    DateTime endDateTime);

        bool Edit(
                  string id,
                  string className,
                  string imageUrl,
                  int maxPractitionerCount,
                  decimal price,
                  DateTime startDate,
                  DateTime endDate,
                  int trainerId
                  );


        bool Delete(string id);

        

        AllClassViewModel GetCountOfAllClasses();

        ClassFormModel GetClassById(string id);

        ClassDetailsViewModel GetClassForDetails(string id);

        IEnumerable<ClassFormModelForAdmin> GetAllClassesForAdmin();

        IEnumerable<ClassViewModel> GetAllClasses();

        IEnumerable<ClassViewModel> GetAllFitnessClass();

        IEnumerable<ClassViewModel> GetAllYogaClass();

        IEnumerable<ClassViewModel> GetAllMmaClass();

        IEnumerable<ClassViewModel> GetAllBoxClass();

        IEnumerable<ClassViewModel> GetAllWrestlingClass();

        IEnumerable<ClassViewModel> GetAllAthleticClass();

        IEnumerable<TrainerClassViewModel> GetAllTrainers();

        IEnumerable<ClassTrainerViewModel> GetAllTrainerClasses(int trainerId);


    }
}
