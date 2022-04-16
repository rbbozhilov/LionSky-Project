using LionSkyNot.Areas.Admin.Controllers;
using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Models.Class;
using LionSkyNot.Services.Classes;
using LionSkyNot.Tests.Mock;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LionSkyNot.Tests.Controllers.AdminControllers
{
    public class ClassControllerTest
    {

        [Fact]
        public void ShowClasses_ShouldReturnCorrectViewResultAndViewModel()
        {
            //Arrange

            using var data = DatabaseMock.Instance;
            var classService = new ClassService(data);
            var classController = new ClassController(classService);
            var classModel = new List<ClassFormModelForAdmin>();

            var categoryBox = new Categorie()
            {
                Id = 1,
                Name = "Box"
            };


            var trainerBox1 = new Trainer()
            {
                FullName = "Connor",
                BirthDate = DateTime.Now,
                Description = "Best champ ever",
                YearOfExperience = 20,
                Categorie = categoryBox,
                ImageUrl = "Some image",
                UserId = "SomeUser",
            };

            var trainerBox2 = new Trainer()
            {
                FullName = "Connor2",
                BirthDate = DateTime.Now,
                Description = "Best champ ever2",
                YearOfExperience = 5,
                Categorie = categoryBox,
                ImageUrl = "Some2 image",
                UserId = "SomeU2ser",
            };

            var class1 = new Class()
            {
                Id = "id1",
                ClassName = "Box class",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now,
                ImageUrl = "SomeImg",
                MaxPractitionerCount = 2,
                Trainer = trainerBox1,
            };

            var class2 = new Class()
            {
                Id = "id2",
                ClassName = "Box class1",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now,
                ImageUrl = "SomeImg1",
                MaxPractitionerCount = 21,
                Trainer = trainerBox2,
            };

            data.Classes.AddRange(class1, class2);
            data.SaveChanges();


            //Act

            var result = classController.ShowClasses(classModel);

            //Assert

            var viewModel = Assert.IsType<ViewResult>(result);
            var currentClasses = Assert.IsType<List<ClassFormModelForAdmin>>(viewModel.Model);

            Assert.Equal("Box class", currentClasses.First().ClassName);
            Assert.Equal("id1", currentClasses.First().Id);

            Assert.Equal("Box class1", currentClasses.Last().ClassName);
            Assert.Equal("id2", currentClasses.Last().Id);

        }

    }

}
