using LionSkyNot.Data.Models.Classes;

using LionSkyNot.Models.Class;

using LionSkyNot.Views.ViewModels.Classes;


namespace LionSkyNot.Services.Classes
{
    public interface IClassService
    {

        Task CreateAsync(
                    string fullname,
                    string imageUrl,
                    int MaxPractitionerCount,
                    int trainerId,
                    DateTime startDateTime,
                    DateTime endDateTime);


        Task<bool> EditAsync(
                  string id,
                  string className,
                  string imageUrl,
                  int maxPractitionerCount,
                  DateTime startDate,
                  DateTime endDate,
                  int trainerId
                  );


        Task<bool> DeleteAsync(string id);

        bool IsHaveTrainerById(int id);

        Task<bool> RemovingClassFromUserAsync(string userId, string classId);

        Task<bool> AddUserToClassAsync(string userId, string classId);

        bool CheckFreePlace(string classId);

        bool IsUserHaveClasses(string userId);

        AllClassViewModel GetCountOfAllClasses();

        ClassFormModel GetClassById(string id);

        ClassDetailsViewModel GetClassForDetails(string id);

        IEnumerable<ClassFormModelForAdmin> GetAllClassesForAdmin();

        IEnumerable<ClassViewModel> GetUserClasses(string userId);

        IEnumerable<ClassViewModel> GetAllClasses();

        IEnumerable<ClassViewModel> GetAllClassesByCategorieName(string categorieName);

        IEnumerable<TrainerClassViewModel> GetAllTrainers();

        IEnumerable<ClassTrainerViewModel> GetAllTrainerClasses(int trainerId);

    }
}
