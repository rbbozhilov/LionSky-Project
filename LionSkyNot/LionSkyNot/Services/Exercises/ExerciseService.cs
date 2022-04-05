using LionSkyNot.Data;

using LionSkyNot.Data.Models.Exercise;

using LionSkyNot.Models.Exercises;

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



        public void Create(
                           string name,
                           string imgUrl,
                           string videoUrl,
                           int typeExercisesId)
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


        public bool Edit(
                         int id,
                         string name,
                         string imgUrl,
                         string videoUrl)
        {
            var exercise = this.data.Exercises
                                    .Where(e => e.Id == id && e.IsDeleted == false)
                                    .FirstOrDefault();

            if (exercise == null)
            {
                return false;
            }


            exercise.Name = name;
            exercise.ImageUrl = imgUrl;
            exercise.VideoUrl = videoUrl;

            this.data.SaveChanges();

            return true;

        }


        public bool Delete(int id)
        {
            var currentExercise = this.data.Exercises
                                           .Where(e => e.Id == id && e.IsDeleted == false)
                                           .FirstOrDefault();
            if (currentExercise == null)
            {
                return false;
            }

            currentExercise.IsDeleted = true;

            this.data.SaveChanges();

            return true;
        }


        public EditExerciseFormModel GetExerciseById(int id)
        => this.data.Exercises
                    .Where(e => e.Id == id && e.IsDeleted == false)
                    .Select(e => new EditExerciseFormModel()
                    {
                        ImageUrl = e.ImageUrl,
                        Name = e.Name,
                        VideoUrl = e.VideoUrl
                    })
                    .FirstOrDefault();


        public IEnumerable<ExerciseFormModelForAdmin> GetAllExercises()
        => this.data.Exercises
                     .Where(e => e.IsDeleted == false)
                     .Select(e => new ExerciseFormModelForAdmin()
                     {
                         Id = e.Id,
                         Name = e.Name,
                     })
                     .ToList();


        public IEnumerable<ExerciseViewModel> GetAllExercisesByType(string type)
        => this.data.Exercises
                    .Where(x => x.IsDeleted == false && x.TypeExercise.TypeName == type)
                    .Select(x => new ExerciseViewModel
                    {
                        Name = x.Name,
                        ImageUrl = x.ImageUrl,
                        VideoUrl = x.VideoUrl
                    })
                    .ToList();


        public IEnumerable<TypeExerciseViewModel> GetAllTypeExercises()
        => this.data.TypeExercises
                    .Select(x => new TypeExerciseViewModel
                    {
                        Id = x.Id,
                        Name = x.TypeName
                    });

    }
}
