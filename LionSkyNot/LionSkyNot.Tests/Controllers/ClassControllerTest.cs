using System.Collections.Generic;

using System.Linq;

using LionSkyNot.Controllers;

using LionSkyNot.Data.Models.Classes;

using LionSkyNot.Services.Classes;

using LionSkyNot.Tests.Mock;

using LionSkyNot.Views.ViewModels.Classes;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace LionSkyNot.Tests.Controllers
{
    public class ClassControllerTest
    {

        private Categorie categorie1;
        private Categorie categorie2;
        private Trainer trainer1;
        private Trainer trainer2;
        private Class class1;
        private Class class2;


        public ClassControllerTest()
        {
            this.categorie1 = new Categorie
            {
                Id = 1,
                Name = "Fitness"
            };

            this.categorie2 = new Categorie
            {
                Id = 2,
                Name = "Box"
            };

            this.trainer1 = new Trainer()
            {
                FullName = "sometrainer",
                Id = 1,
                Description = "some description",
                ImageUrl = "img url",
                Categorie = categorie1,
                UserId = "someuserid"

            };

            this.trainer2 = new Trainer()
            {
                FullName = "sometrainer2",
                Id = 2,
                Description = "some description2",
                ImageUrl = "img ur2l",
                Categorie = categorie2,
                UserId = "someuserid2"

            };


            this.class1 = new Class()
            {
                ClassName = "someclass",
                Id = "someid",
                ImageUrl = "someimg",
                Trainer = trainer1
            };

            this.class2 = new Class()
            {
                ClassName = "someclass2",
                Id = "someid2",
                ImageUrl = "someimg2",
                Trainer = trainer2
            };
        }


        [Fact]
        public void Index_Should_Return_CountOfAllClasses_And_ViewResultCorrect()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);

            var classController = new ClassController(classService);

            var allClassModel = new AllClassViewModel();

            //Act

            var result = classController.Index(allClassModel);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var countOfAllClasses = Assert.IsType<AllClassViewModel>(viewModel.Model);

            Assert.Equal(0, countOfAllClasses.CountOfFitnessClass);
            Assert.Equal(0, countOfAllClasses.CountOfAthleticClass);
            Assert.Equal(0, countOfAllClasses.CountOfBoxClass);
            Assert.Equal(0, countOfAllClasses.CountOfMmaClass);
            Assert.Equal(0, countOfAllClasses.CountOfWrestlingClass);
            Assert.Equal(0, countOfAllClasses.CountOfYogaClass);

        }


        [Fact]
        public void ViewAllFitnessClassesShouldReturnCorrectClasses()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);

            var classController = new ClassController(classService);

            data.Categories.AddRange(this.categorie1, this.categorie2);
            data.SaveChanges();

            data.Trainers.AddRange(this.trainer1, this.trainer2);

            data.Classes.AddRange(this.class1, this.class2);

            data.SaveChanges();

            //Act

            var result = classController.ViewAllFitnessClass();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allClasses = Assert.IsType<List<ClassViewModel>>(viewModel.Model);

            Assert.Equal("someid", allClasses.First().Id);

        }


        [Fact]
        public void ViewAllBoxClassesShouldReturnCorrectClasses()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);

            var classController = new ClassController(classService);

            data.Categories.AddRange(this.categorie1, this.categorie2);
            data.SaveChanges();

            data.Trainers.AddRange(this.trainer1, this.trainer2);

            data.Classes.AddRange(this.class1, this.class2);

            data.SaveChanges();

            //Act

            var result = classController.ViewAllBoxClass();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allClasses = Assert.IsType<List<ClassViewModel>>(viewModel.Model);

            Assert.Equal("someid2", allClasses.First().Id);

        }

        [Fact]
        public void ViewAllYogaClassesShouldReturnCorrectClasses()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);

            var classController = new ClassController(classService);
            this.categorie1.Name = "Yoga";
            data.Categories.AddRange(this.categorie1, this.categorie2);
            data.SaveChanges();

            data.Trainers.AddRange(this.trainer1, this.trainer2);

            data.Classes.AddRange(this.class1, this.class2);

            data.SaveChanges();

            //Act

            var result = classController.ViewAllYogaClass();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allClasses = Assert.IsType<List<ClassViewModel>>(viewModel.Model);

            Assert.Equal("someid", allClasses.First().Id);

        }

        [Fact]
        public void ViewAllWrestlingClassesShouldReturnCorrectClasses()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);

            var classController = new ClassController(classService);
            this.categorie1.Name = "Wrestling";
            data.Categories.AddRange(this.categorie1, this.categorie2);
            data.SaveChanges();

            data.Trainers.AddRange(this.trainer1, this.trainer2);

            data.Classes.AddRange(this.class1, this.class2);

            data.SaveChanges();

            //Act

            var result = classController.ViewAllWrestlingClass();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allClasses = Assert.IsType<List<ClassViewModel>>(viewModel.Model);

            Assert.Equal("someid", allClasses.First().Id);

        }

        [Fact]
        public void ViewAllMmaClassesShouldReturnCorrectClasses()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);

            var classController = new ClassController(classService);
            this.categorie1.Name = "Mma";
            data.Categories.AddRange(this.categorie1, this.categorie2);
            data.SaveChanges();

            data.Trainers.AddRange(this.trainer1, this.trainer2);

            data.Classes.AddRange(this.class1, this.class2);

            data.SaveChanges();

            //Act

            var result = classController.ViewAllMmaClass();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allClasses = Assert.IsType<List<ClassViewModel>>(viewModel.Model);

            Assert.Equal("someid", allClasses.First().Id);

        }

        [Fact]
        public void ViewAllAthleticClassesShouldReturnCorrectClasses()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);

            var classController = new ClassController(classService);

            this.categorie2.Name = "Athletic";
            data.Categories.AddRange(this.categorie1, this.categorie2);
            data.SaveChanges();

            
            data.Trainers.AddRange(this.trainer1, this.trainer2);

            data.Classes.AddRange(this.class1, this.class2);

            data.SaveChanges();

            //Act

            var result = classController.ViewAllAthleticClass();

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var allClasses = Assert.IsType<List<ClassViewModel>>(viewModel.Model);

            Assert.Equal("someid2", allClasses.First().Id);

        }


        [Fact]
        public void ViewDetailsClass_ShouldReturnCorrectClassGettedById()
        {

            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);

            var classController = new ClassController(classService);

            data.Categories.AddRange(this.categorie1, this.categorie2);
            data.SaveChanges();

            data.Trainers.AddRange(this.trainer1, this.trainer2);

            data.Classes.AddRange(this.class1, this.class2);

            data.SaveChanges();

            //Act

            var result = classController.ViewDetails("someid");

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var classDetail = Assert.IsType<ClassDetailsViewModel>(viewModel.Model);

            Assert.Equal("sometrainer", classDetail.TrainerName);
            Assert.Equal("someclass", classDetail.ClassName);

        }
    }
}
