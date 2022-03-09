using LionSkyNot.Data;
using LionSkyNot.Data.Models.Exercise;
using LionSkyNot.Views.ViewModels.Exercises;

namespace LionSkyNot.Services.Exercises
{
    public class ExerciseService : IExerciseService
    {

        private LionSkyDbContext data;

        public ExerciseService(LionSkyDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<ExerciseViewModel> GetAllExercisesByType(string type)
        {
            return this.data.Exercises
                            .Where(x=> x.IsDeleted == false && x.TypeExercise.TypeName == type)
                            .Select(x=> new ExerciseViewModel
                            {
                                Name = x.Name,
                                ImageUrl = x.ImageUrl,
                                VideoUrl = x.VideoUrl
                            })
                            .ToList();
        }

        public IEnumerable<TypeExerciseViewModel> GetAllTypeExercises()
        => this.data.TypeExercises.Select(x => new TypeExerciseViewModel
        {
            Id = x.Id,
            Name = x.TypeName
        });

        public void Create(string name,string imgUrl, string videoUrl,int typeExercisesId)
        {
            var exercise = new Exercise()
            {
                Name = name,
                ImageUrl = imgUrl,
                VideoUrl = videoUrl,
                TypeExerciseId = typeExercisesId
                
            };

            this.data.Exercises.Add(exercise);

            this.data.SaveChanges();

        }
    }
}
