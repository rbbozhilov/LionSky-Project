using LionSkyNot.Controllers;

using LionSkyNot.Models.Exercises;

using LionSkyNot.Services.Exercises;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using static LionSkyNot.Areas.Admin.AdminConstants;


namespace LionSkyNot.Areas.Admin.Controllers
{

    [Area(AreaName)]
    public class ExerciseController : BaseController
    {

        private IExerciseService exerciseService;


        public ExerciseController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }



        [Authorize(Roles = ModeratorAndAdminRole)]
        public IActionResult AddExercise()
        {
            return View(new AddExerciseFormModel()
            {
                Type = this.exerciseService.GetAllTypeExercises()
            });
        }


        [Authorize(Roles = ModeratorAndAdminRole)]
        [HttpPost]
        public async Task<IActionResult> AddExercise(AddExerciseFormModel exerciseModel)
        {

            if (!this.exerciseService.AnyExercieByType(exerciseModel.TypeId))
            {
                this.ModelState.AddModelError(nameof(exerciseModel.TypeId), "Don't make some hack tries!");
            }

            if (!ModelState.IsValid)
            {
                exerciseModel.Type = this.exerciseService.GetAllTypeExercises();
                return View(exerciseModel);
            }

            await this.exerciseService.CreateAsync(
                                                   exerciseModel.Name,
                                                   exerciseModel.ImageUrl,
                                                   exerciseModel.Description,
                                                   exerciseModel.TypeId);


            return RedirectToAction("SuccessChange");
        }


        [Authorize(Roles = AdminRole)]
        public IActionResult ShowExercise(IEnumerable<ExerciseFormModelForAdmin> exerciseModel)
        {

            exerciseModel = this.exerciseService.GetAllExercises();

            return View(exerciseModel);
        }


        [Authorize(Roles = ModeratorAndAdminRole)]
        public IActionResult SuccessChange()
        => View();

        [Authorize(Roles = AdminRole)]
        public IActionResult EditExercise(int id)
        {

            var exercise = this.exerciseService.GetExerciseById(id);

            if (exercise == null)
            {
                return BadRequest();
            }

            return View(new EditExerciseFormModel()
            {
                Description = exercise.Description,
                ImageUrl = exercise.ImageUrl,
                Name = exercise.Name
            });
        }


        [Authorize(Roles = AdminRole)]
        [HttpPost]
        public async Task<IActionResult> EditExercise(EditExerciseFormModel exerciseModel, int id)
        {

            if (!ModelState.IsValid)
            {
                return View(exerciseModel);
            }

            var isEditted = await this.exerciseService.EditAsync(
                                                       id,
                                                       exerciseModel.Name,
                                                       exerciseModel.ImageUrl,
                                                       exerciseModel.Description);

            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction("SuccessChange");
        }


        [Authorize(Roles = AdminRole)]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var isDeleted = await this.exerciseService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest();
            };

            return View("SuccessChange");
        }

    }
}
