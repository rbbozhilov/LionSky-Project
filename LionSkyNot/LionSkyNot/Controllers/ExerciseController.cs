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
        => View();


        public IActionResult BackExercises(IEnumerable<ExerciseViewModel> exerciseViewModel)
         => View(this.exerciesService.GetAllExercisesByType("Back"));


        public IActionResult LegExercises(IEnumerable<ExerciseViewModel> exerciseViewModel)
        => View(this.exerciesService.GetAllExercisesByType("Legs"));


        public IActionResult BicepsExercises(IEnumerable<ExerciseViewModel> exerciseViewModel)
        => View(this.exerciesService.GetAllExercisesByType("Biceps"));


        public IActionResult ChestExercises(IEnumerable<ExerciseViewModel> exerciseViewModel)
        => View(this.exerciesService.GetAllExercisesByType("Chest"));

    }
}
