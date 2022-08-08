using System.Linq;

using LionSkyNot.Data.Models.Exercise;

using LionSkyNot.Services.Exercises;

using LionSkyNot.Tests.Mock;

using Xunit;


namespace LionSkyNot.Tests.Services
{
    public class ExerciseServiceTest
    {

        private int id;
        private int id2;
        private string name;
        private string name2;
        private string image;
        private string image2;
        private string video;
        private string video2;

        private Exercise exercise;
        private Exercise exercise1;

        public ExerciseServiceTest()
        {
            this.id = 1;
            this.id2 = 2;
            this.name2 = "test2";
            this.image2 = "image2";
            this.video2 = "video2";
            this.name = "test";
            this.image = "image";
            this.video = "video";

            this.exercise = new Exercise()
            {
                Id = id,
                Name = name,
                ImageUrl = image,
                Description = video,
                TypeExercise = new TypeExercise() { Id = 1, TypeName = "Biceps" }
            };

            exercise1 = new Exercise()
            {
                Id = 2,
                Name = "gggg",
                ImageUrl = "igerq",
                Description = "vidd",
                TypeExerciseId = 2
            };
        }


        [Fact]
        public void CreateExercise_ShouldBeCorrect()
        {

            //Arrange

            var typeExerciseId = 1;

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);


            //Act

            exerciseService.Create(
                                   this.name,
                                   this.image,
                                   this.video,
                                   typeExerciseId);

            var actualExercise = data.Exercises.Where(t => t.Name == this.name);

            //Assert

            Assert.NotNull(actualExercise);

        }


        [Fact]
        public void EditExercise_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);


            //Assert

            Assert.False(exerciseService.Edit(id, name, image, video));

        }


        [Fact]
        public void EditExercise_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);


            //Act

            data.Exercises.Add(exercise);
            data.SaveChanges();

            var result = exerciseService.Edit(id, name2, image2, video2);

            //Assert

            Assert.True(result);
            Assert.NotEqual(name, exercise.Name);
            Assert.NotEqual(image, exercise.Description);
            Assert.NotEqual(video, exercise.Description);

        }


        [Fact]
        public void DeleteExercise_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);


            //Assert

            Assert.False(exerciseService.Delete(1));

        }


        [Fact]
        public void DeleteExercise_ShouldBeSuccess()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);

            //Act

            data.Exercises.Add(exercise);
            data.SaveChanges();

            //Assert

            Assert.True(exerciseService.Delete(id));
            Assert.True(exercise.IsDeleted);

        }


        [Fact]
        public void GetExerciseById_ShouldReturnNull()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);


            //Assert

            Assert.Null(exerciseService.GetExerciseById(1));

        }


        [Fact]
        public void GetExerciseById_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);

            //Act

            data.Exercises.Add(exercise);
            data.SaveChanges();

            //Assert

            Assert.Equal(name, exerciseService.GetExerciseById(1).Name);

        }


        [Fact]
        public void GetAllExercises_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);

            //Act

            data.Exercises.AddRange(exercise, exercise1);
            data.SaveChanges();

            var allExercises = exerciseService.GetAllExercises();

            //Assert

            Assert.True(allExercises.Any(e => e.Id == id));
            Assert.True(allExercises.Any(e => e.Id == 2));
            Assert.Equal(2, allExercises.Count());

        }


        [Fact]
        public void GetAllExerciseByType_ShouldBeCorrect()
        {

            //Arrange

            exercise.TypeExercise = new TypeExercise() { Id = 1, TypeName = "Biceps" };

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);

            //Act

            data.Exercises.AddRange(exercise, exercise1);
            data.SaveChanges();

            var allExercisesByType = exerciseService.GetAllExercisesByType("Biceps");

            //Assert

            Assert.Equal(name, allExercisesByType.First().Name);
            Assert.Equal(1, allExercisesByType.Count());

        }


        [Fact]
        public void GetAllTypesExercises_ShouldBeCorrect()
        {

            //Arrange

            var type1 = new TypeExercise() { Id = 1, TypeName = "Biceps" };
            var type2 = new TypeExercise() { Id = 2, TypeName = "Back" };

            using var data = DatabaseMock.Instance;
            var exerciseService = new ExerciseService(data);

            //Act

            data.TypeExercises.AddRange(type1, type2);
            data.SaveChanges();

            var allTypes = exerciseService.GetAllTypeExercises();

            //Assert

            Assert.Equal(2, allTypes.Count());
            Assert.True(allTypes.Any(t => t.Name == "Biceps"));
            Assert.True(allTypes.Any(t => t.Name == "Back"));

        }
    }
}
