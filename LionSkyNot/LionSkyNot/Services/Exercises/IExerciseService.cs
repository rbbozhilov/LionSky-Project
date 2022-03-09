using LionSkyNot.Data.Models.Exercise;
using LionSkyNot.Views.ViewModels.Exercises;

namespace LionSkyNot.Services.Exercises
{
    public interface IExerciseService
    {

        IEnumerable<TypeExerciseViewModel> GetAllTypeExercises();

        IEnumerable<ExerciseViewModel> GetAllExercisesByType(string type);

        void Create(string name, string imgUrl, string videoUrl, int typeExerciseId);

    }
}
