using LionSkyNot.Controllers;

using LionSkyNot.Models.Class;

using LionSkyNot.Services.Classes;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using static LionSkyNot.Areas.Admin.AdminConstants;


namespace LionSkyNot.Areas.Admin.Controllers
{

    [Area(AreaName)]
    public class ClassController : BaseController
    {

        private IClassService classService;


        public ClassController(IClassService classService)
        {
            this.classService = classService;
        }



        [Authorize(Roles = AdminRole)]
        public IActionResult ShowClasses(IEnumerable<ClassFormModelForAdmin> classModel)
        {
            classModel = this.classService.GetAllClassesForAdmin();

            return View(classModel);
        }


        [Authorize(Roles = ModeratorAndAdminRole)]
        public IActionResult AddClass()
        {
            return View(new ClassFormModel()
            {
                Trainers = this.classService.GetAllTrainers()
            });

        }


        [Authorize(Roles = ModeratorAndAdminRole)]
        [HttpPost]
        public IActionResult AddClass(ClassFormModel classModel)
        {

            if (classModel.StartDateTime > classModel.EndDateTime)
            {
                this.ModelState.AddModelError("errorDate", "Cannot start date be after end date");
            }


            if (!classService.IsHaveTrainerById(classModel.TrainerId))
            {
                this.ModelState.AddModelError(nameof(classModel.TrainerId), "Don't make some hack tries!");
            }


            if (!ModelState.IsValid)
            {
                classModel.Trainers = this.classService.GetAllTrainers();


                return View(classModel);
            }

            this.classService.Create(
                                     classModel.ClassName,
                                     classModel.ImageUrl,
                                     classModel.MaxPractitionerCount,
                                     classModel.TrainerId,
                                     classModel.StartDateTime,
                                     classModel.EndDateTime);

            return RedirectToAction("Successfull");
        }


        [Authorize(Roles = AdminRole)]
        public IActionResult Successfull()
        {
            return View();
        }


        [Authorize(Roles = AdminRole)]
        public IActionResult EditClass(string id)
        {

            var currentClass = this.classService.GetClassById(id);

            if (currentClass == null)
            {
                return BadRequest();
            }

            currentClass.Trainers = this.classService.GetAllTrainers();

            return View(new ClassFormModel()
            {
                ClassName = currentClass.ClassName,
                TrainerId = currentClass.TrainerId,
                Trainers = currentClass.Trainers,
                ImageUrl = currentClass.ImageUrl,
                MaxPractitionerCount = currentClass.MaxPractitionerCount,
                StartDateTime = currentClass.StartDateTime,
                EndDateTime = currentClass.EndDateTime,

            });

        }


        [Authorize(Roles = AdminRole)]
        [HttpPost]
        public IActionResult EditClass(ClassFormModel classModel, string id)
        {

            if (!ModelState.IsValid)
            {
                return View(classModel);
            }

            bool isEditted = this.classService.Edit(
                                                    id,
                                                    classModel.ClassName,
                                                    classModel.ImageUrl,
                                                    classModel.MaxPractitionerCount,
                                                    classModel.StartDateTime,
                                                    classModel.EndDateTime,
                                                    classModel.TrainerId);


            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction("Successfull");

        }


        [Authorize(Roles = AdminRole)]
        public IActionResult DeleteClass(string id)
        {

            bool isDeleted = this.classService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return View("Successfull");
        }

    }
}
