using LionSkyNot.Data.Models.Exercise;

using LionSkyNot.Models.Exercises;

using LionSkyNot.Views.ViewModels.Exercises;


namespace LionSkyNot.Services.Exercises
{
    public interface IExerciseService
    {

        Task CreateAsync(
                    string name,
                    string imgUrl,
                    string videoUrl,
                    int typeExerciseId);

        Task<bool> DeleteAsync(int id);

        Task<bool> EditAsync(
                  int id,
                  string name,
                  string imgUrl,
                  string videoUrl);

        bool AnyExercieByType(int id);

        EditExerciseFormModel GetExerciseById(int id);

        IEnumerable<TypeExerciseViewModel> GetAllTypeExercises();

        IEnumerable<ExerciseViewModel> GetAllExercisesByType(string type);

        IEnumerable<ExerciseFormModelForAdmin> GetAllExercises();



    }
}
