using LionSkyNot.Data.Models.Exercise;
using LionSkyNot.Models.Exercises;
using LionSkyNot.Views.ViewModels.Exercises;

namespace LionSkyNot.Services.Exercises
{
    public interface IExerciseService
    {

        void Create(string name, string imgUrl, string videoUrl, int typeExerciseId);

        bool Delete(int id);

        bool Edit(int id,string name, string imgUrl, string videoUrl);

        EditExerciseFormModel GetExerciseById(int id);

        IEnumerable<TypeExerciseViewModel> GetAllTypeExercises();

        IEnumerable<ExerciseViewModel> GetAllExercisesByType(string type);

        IEnumerable<ExerciseFormModelForAdmin> GetAllExercises();



    }
}
