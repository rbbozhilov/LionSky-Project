using LionSkyNot.Services.Exercises;
using LionSkyNot.Views.ViewModels.Exercises;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class ExerciseController : BaseController
    {

        private IExerciseService exerciesService;

        public ExerciseController(IExerciseService exerciesService)
        {
            this.exerciesService = exerciesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BackExercises(IEnumerable<ExerciseViewModel> exerciseViewModel)
        {

            exerciseViewModel = this.exerciesService.GetAllExercisesByType("Back");

            return View(exerciseViewModel);
        }

        public IActionResult LegExercises(IEnumerable<ExerciseViewModel> exerciseViewModel)
        {

            exerciseViewModel = this.exerciesService.GetAllExercisesByType("Legs");

            return View(exerciseViewModel);
        }

        public IActionResult BicepsExercises(IEnumerable<ExerciseViewModel> exerciseViewModel)
        {

            exerciseViewModel = this.exerciesService.GetAllExercisesByType("Biceps");

            return View(exerciseViewModel);
        }

        public IActionResult ChestExercises(IEnumerable<ExerciseViewModel> exerciseViewModel)
        {

            exerciseViewModel = this.exerciesService.GetAllExercisesByType("Chest");

            return View(exerciseViewModel);
        }

    }
}
