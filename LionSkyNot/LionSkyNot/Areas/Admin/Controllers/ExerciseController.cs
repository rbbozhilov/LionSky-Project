﻿using LionSkyNot.Controllers;
using LionSkyNot.Models.Exercises;
using LionSkyNot.Services.Exercises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Areas.Admin.Controllers
{

    [Area(AdminConstants.AreaName)]
    public class ExerciseController : BaseController
    {

        private IExerciseService exerciseService;


        public ExerciseController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }



        [Authorize(Roles = "Moderator,Administrator")]
        public IActionResult AddExercise()
        {
            return View(new AddExerciseFormModel()
            {
                Type = this.exerciseService.GetAllTypeExercises()
            });
        }


        [Authorize(Roles = "Moderator,Administrator")]
        [HttpPost]
        public IActionResult AddExercise(AddExerciseFormModel exerciseModel)
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

            this.exerciseService.Create(exerciseModel.Name, exerciseModel.ImageUrl, exerciseModel.VideoUrl, exerciseModel.TypeId);


            return RedirectToAction("SuccessChange");
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult ShowExercise(IEnumerable<ExerciseFormModelForAdmin> exerciseModel)
        {

            exerciseModel = this.exerciseService.GetAllExercises();

            return View(exerciseModel);
        }


        [Authorize(Roles = "Moderator,Administrator")]
        public IActionResult SuccessChange()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditExercise(int id)
        {

            var exercise = this.exerciseService.GetExerciseById(id);

            if (exercise == null)
            {
                return BadRequest();
            }

            return View(new EditExerciseFormModel()
            {
                VideoUrl = exercise.VideoUrl,
                ImageUrl = exercise.ImageUrl,
                Name = exercise.Name
            });
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult EditExercise(EditExerciseFormModel exerciseModel, int id)
        {

            if (!ModelState.IsValid)
            {
                return View(exerciseModel);
            }


            bool isEditted = this.exerciseService.Edit(
                                                       id,
                                                       exerciseModel.Name,
                                                       exerciseModel.ImageUrl,
                                                       exerciseModel.VideoUrl);

            if (!isEditted)
            {
                return BadRequest();
            }


            return RedirectToAction("SuccessChange");
        }


        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteExercise(int id)
        {

            if (!this.exerciseService.Delete(id))
            {
                return BadRequest();
            };

            return View("SuccessChange");
        }

    }
}
