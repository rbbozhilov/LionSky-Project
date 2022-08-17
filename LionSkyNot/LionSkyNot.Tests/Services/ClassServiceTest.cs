using System;
using System.Linq;
using System.Threading.Tasks;
using LionSkyNot.Data.Models.Classes;

using LionSkyNot.Services.Classes;

using LionSkyNot.Tests.Mock;

using Xunit;


namespace LionSkyNot.Tests.Services
{
    public class ClassServiceTest
    {

        [Fact]
        public async Task AddUserToClass_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var userId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            await classService.CreateAsync(
                                 className,
                                 imgUrl,
                                 maxPractitionerCount,
                                 trainerId,
                                 startDate,
                                 endDate);

            var classId = data.Classes
                                 .Where(c => c.ClassName == className)
                                 .Select(c => c.Id)
                                 .FirstOrDefault();



            var result = await classService.AddUserToClassAsync(userId, classId);

            var countOfPractitioners = data.Classes
                                      .Where(c => c.Id == classId)
                                      .Select(c => c.PractitionerCount)
                                      .FirstOrDefault();
            //Assert

            Assert.True(result);
            Assert.Equal(1, countOfPractitioners);

        }


        [Fact]
        public async Task AddUserToClass_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var userId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            await classService.CreateAsync(
                                className,
                                imgUrl,
                                maxPractitionerCount,
                                trainerId,
                                startDate,
                                endDate);

            var classId = data.Classes
                                 .Where(c => c.ClassName == className)
                                 .Select(c => c.Id)
                                 .FirstOrDefault();

            await classService.AddUserToClassAsync(userId, classId);

            //Assert

            Assert.False(await classService.AddUserToClassAsync(userId, classId));

        }


        [Fact]
        public async Task CheckFreePlace_ShouldBeFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var userId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            await classService.CreateAsync(
                                 className,
                                 imgUrl,
                                 maxPractitionerCount,
                                 trainerId,
                                 startDate,
                                 endDate);

            var classId = data.Classes
                                 .Where(c => c.ClassName == className)
                                 .Select(c => c.Id)
                                 .FirstOrDefault();

            //Assert

            Assert.False(classService.CheckFreePlace(classId));


        }


        [Fact]
        public async Task CheckFreePlace_ShouldBeTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var userId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 1;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            await classService.CreateAsync(
                                  className,
                                  imgUrl,
                                  maxPractitionerCount,
                                  trainerId,
                                  startDate,
                                  endDate);

            var classId = data.Classes
                                 .Where(c => c.ClassName == className)
                                 .Select(c => c.Id)
                                 .FirstOrDefault();

            await classService.AddUserToClassAsync(userId, classId);

            //Assert

            Assert.True(classService.CheckFreePlace(classId));


        }


        [Fact]
        public void IsUserHaveClasses_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var userId = "someId";

            //Assert

            Assert.False(classService.IsUserHaveClasses(userId));

        }


        [Fact]
        public async Task IsUserHaveClasses_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var userId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            await classService.CreateAsync(
                                 className,
                                 imgUrl,
                                 maxPractitionerCount,
                                 trainerId,
                                 startDate,
                                 endDate);

            var classId = data.Classes
                                 .Where(c => c.ClassName == className)
                                 .Select(c => c.Id)
                                 .FirstOrDefault();

            await classService.AddUserToClassAsync(userId, classId);

            //Assert

            Assert.True(classService.IsUserHaveClasses(userId));

        }


        [Fact]
        public async Task Create_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            await classService.CreateAsync(
                                 className,
                                 imgUrl,
                                 maxPractitionerCount,
                                 trainerId,
                                 startDate,
                                 endDate);

            //Assert

            Assert.True(data.Classes.Any(c => c.ClassName == className));

        }


        [Fact]
        public async Task Edit_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var classId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Assert

            Assert.False(await classService.EditAsync(
                                           classId,
                                           className,
                                           imgUrl,
                                           maxPractitionerCount,
                                           startDate,
                                           endDate,
                                           trainerId));

        }


        [Fact]
        public async Task Edit_ShouldReturnTrueAndBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            var className1 = "classNAME1";
            var imgUrl1 = "someIm1g";
            var maxPractitionerCount1 = 51;
            var trainerId1 = 51;
            var startDate1 = DateTime.Now;
            var endDate1 = DateTime.Now;

            //Act

            await classService.CreateAsync(
                                className,
                                imgUrl,
                                maxPractitionerCount,
                                trainerId,
                                startDate,
                                endDate);

            var classId = data.Classes
                                 .Where(c => c.ClassName == className)
                                 .Select(c => c.Id)
                                 .FirstOrDefault();

            var result = await classService.EditAsync(
                                           classId,
                                           className1,
                                           imgUrl1,
                                           maxPractitionerCount1,
                                           startDate1,
                                           endDate1,
                                           trainerId1);

            var currentClass = data.Classes
                                   .Where(c => c.Id == classId)
                                   .FirstOrDefault();

            //Assert
            Assert.True(result);
            Assert.Equal(className1, currentClass.ClassName);
            Assert.Equal(imgUrl1, currentClass.ImageUrl);
            Assert.Equal(maxPractitionerCount1, currentClass.MaxPractitionerCount);
            Assert.Equal(startDate1, currentClass.StartDateTime);
            Assert.Equal(endDate1, currentClass.EndDateTime);
            Assert.Equal(trainerId1, currentClass.TrainerId);


        }


        [Fact]
        public async Task Delete_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var classId = "someId";

            //Assert

            Assert.False(await classService.DeleteAsync(classId));

        }


        [Fact]
        public async Task Delete_ShouldBeCorrectAndReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            await classService.CreateAsync(
                                className,
                                imgUrl,
                                maxPractitionerCount,
                                trainerId,
                                startDate,
                                endDate);

            var classId = data.Classes
                                 .Where(c => c.ClassName == className)
                                 .Select(c => c.Id)
                                 .FirstOrDefault();

            var currentClass = data.Classes
                                  .Where(c => c.Id == classId)
                                  .FirstOrDefault();

            //Assert

            Assert.True(await classService.DeleteAsync(classId));
            Assert.True(currentClass.IsDeleted);
        }


        [Fact]
        public async Task RemovingClassFromUser_ShouldReturnFalse()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var userId = "someid";
            var classId = "someId1";


            //Assert

            Assert.False(await classService.RemovingClassFromUserAsync(userId, classId));

        }


        [Fact]
        public async Task RemovingClassFromUser_ShouldReturnTrue()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var userId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            await classService.CreateAsync(
                                 className,
                                 imgUrl,
                                 maxPractitionerCount,
                                 trainerId,
                                 startDate,
                                 endDate);

            var classId = data.Classes
                                 .Where(c => c.ClassName == className)
                                 .Select(c => c.Id)
                                 .FirstOrDefault();

            await classService.AddUserToClassAsync(userId, classId);

            //Assert

            Assert.True(await classService.RemovingClassFromUserAsync(userId, classId));

        }


        [Fact]
        public void GetAllTrainers_ShouldReturnZero_NoTrainers()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);

            //Assert

            Assert.Equal(0, classService.GetAllTrainers().Count());

        }


        [Fact]
        public void GetAllClasses_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var classId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            var newTrainer = new Trainer()
            {
                Id = trainerId,
                FullName = "somename",
                Description = "description",
                ImageUrl = "someimg",
                UserId = "useridd"
            };

            var newClass = new Class()
            {
                Id = classId,
                ClassName = className,
                ImageUrl = imgUrl,
                MaxPractitionerCount = maxPractitionerCount,
                TrainerId = trainerId,
                Trainer = newTrainer,
                StartDateTime = startDate,
                EndDateTime = endDate
            };

            data.Classes.Add(newClass);

            data.SaveChanges();


            var allClasses = classService.GetAllClasses();

            //Assert

            Assert.Equal(1, allClasses.Count());
            Assert.Equal(className, allClasses.First().ClassName);

        }


        [Fact]
        public void GetClassesForAdmin_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var classId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            var newTrainer = new Trainer()
            {
                Id = trainerId,
                FullName = "somename",
                Description = "description",
                ImageUrl = "someimg",
                UserId = "useridd"
            };

            var newClass = new Class()
            {
                Id = classId,
                ClassName = className,
                ImageUrl = imgUrl,
                MaxPractitionerCount = maxPractitionerCount,
                TrainerId = trainerId,
                Trainer = newTrainer,
                StartDateTime = startDate,
                EndDateTime = endDate
            };

            data.Classes.Add(newClass);

            data.SaveChanges();


            var allClasses = classService.GetAllClassesForAdmin();

            //Assert

            Assert.Equal(1, allClasses.Count());
            Assert.Equal(className, allClasses.First().ClassName);

        }


        [Fact]
        public void FindClassById_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var classId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            var newTrainer = new Trainer()
            {
                Id = trainerId,
                FullName = "somename",
                Description = "description",
                ImageUrl = "someimg",
                UserId = "useridd"
            };

            var newClass = new Class()
            {
                Id = classId,
                ClassName = className,
                ImageUrl = imgUrl,
                MaxPractitionerCount = maxPractitionerCount,
                TrainerId = trainerId,
                Trainer = newTrainer,
                StartDateTime = startDate,
                EndDateTime = endDate
            };

            data.Classes.Add(newClass);

            data.SaveChanges();


            var classById = classService.GetClassById(classId);

            //Assert

            Assert.Equal(className, classById.ClassName);

        }


        [Fact]
        public void GetClassForDetails_ShouldReturnCorrectClass()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var classId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            var newTrainer = new Trainer()
            {
                Id = trainerId,
                FullName = "somename",
                Description = "description",
                ImageUrl = "someimg",
                UserId = "useridd"
            };

            var newClass = new Class()
            {
                Id = classId,
                ClassName = className,
                ImageUrl = imgUrl,
                MaxPractitionerCount = maxPractitionerCount,
                TrainerId = trainerId,
                Trainer = newTrainer,
                StartDateTime = startDate,
                EndDateTime = endDate
            };

            data.Classes.Add(newClass);

            data.SaveChanges();


            var classForDetails = classService.GetClassForDetails(classId);

            //Assert

            Assert.Equal(className, classForDetails.ClassName);
            Assert.Equal(imgUrl, classForDetails.ImageUrl);

        }


        [Fact]
        public void GetAllClassesByCategorieName_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var classId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            var newCategorie = new Categorie()
            {
                Id = 1,
                Name = "Fitness"
            };

            var newTrainer = new Trainer()
            {
                Id = trainerId,
                FullName = "somename",
                Description = "description",
                ImageUrl = "someimg",
                UserId = "useridd",
                CategorieId = newCategorie.Id,
                Categorie = newCategorie
            };

            var newClass = new Class()
            {
                Id = classId,
                ClassName = className,
                ImageUrl = imgUrl,
                MaxPractitionerCount = maxPractitionerCount,
                TrainerId = trainerId,
                Trainer = newTrainer,
                StartDateTime = startDate,
                EndDateTime = endDate
            };

            data.Classes.Add(newClass);

            data.SaveChanges();


            var allFitnessClass = classService.GetAllClassesByCategorieName("Fitness");

            //Assert

            Assert.Equal(1, allFitnessClass.Count());
            Assert.Equal(className, allFitnessClass.First().ClassName);

        }


        [Fact]
        public void GetCountOfAllClasses_ShouldBeCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var classId = "someid";
            var className = "classNAME";
            var imgUrl = "someImg";
            var maxPractitionerCount = 5;
            var trainerId = 5;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            //Act

            var newCategorie = new Categorie()
            {
                Id = 1,
                Name = "Fitness"
            };

            var newTrainer = new Trainer()
            {
                Id = trainerId,
                FullName = "somename",
                Description = "description",
                ImageUrl = "someimg",
                UserId = "useridd",
                CategorieId = newCategorie.Id,
                Categorie = newCategorie
            };

            var newClass = new Class()
            {
                Id = classId,
                ClassName = className,
                ImageUrl = imgUrl,
                MaxPractitionerCount = maxPractitionerCount,
                TrainerId = trainerId,
                Trainer = newTrainer,
                StartDateTime = startDate,
                EndDateTime = endDate
            };

            data.Classes.Add(newClass);

            data.SaveChanges();


            var allCountOfClasses = classService.GetCountOfAllClasses();

            //Assert

            Assert.Equal(1, allCountOfClasses.CountOfFitnessClass);
            Assert.Equal(0, allCountOfClasses.CountOfYogaClass);
            Assert.Equal(0, allCountOfClasses.CountOfWrestlingClass);
            Assert.Equal(0, allCountOfClasses.CountOfBoxClass);
            Assert.Equal(0, allCountOfClasses.CountOfMmaClass);
            Assert.Equal(0, allCountOfClasses.CountOfAthleticClass);


        }

    }
}
