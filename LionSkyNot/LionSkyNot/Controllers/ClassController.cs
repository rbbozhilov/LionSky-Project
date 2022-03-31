using LionSkyNot.Services.Classes;

using LionSkyNot.Views.ViewModels.Classes;

using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{
    public class ClassController : BaseController 
    {

        private IClassService classService;

        public ClassController(IClassService classService)
        {
            this.classService = classService;
        }


        public IActionResult Index(AllClassViewModel allClassModel)
        {

            allClassModel = this.classService.GetCountOfAllClasses();

            return View(allClassModel);
        }


        public IActionResult ViewAllFitnessClass()
        {

            var allFitnessClasses = this.classService.GetAllFitnessClass();

            return View(allFitnessClasses);
        }

        public IActionResult ViewAllYogaClass()
        {
            var allYogaClasses = this.classService.GetAllYogaClass();

            return View(allYogaClasses);
        }

        public IActionResult ViewAllMmaClass()
        {
            var allMmaClass = this.classService.GetAllMmaClass();

            return View(allMmaClass);
        }

        public IActionResult ViewAllBoxClass()
        {
            var allBoxClass = this.classService.GetAllBoxClass();

            return View(allBoxClass);
        }

        public IActionResult ViewAllWrestlingClass()
        {
            var allWrestlingClass = this.classService.GetAllWrestlingClass();

            return View(allWrestlingClass);
        }

        public IActionResult ViewAllAthleticClass()
        {
            var allAthleticClass = this.classService.GetAllAthleticClass();

            return View(allAthleticClass);
        }

        public IActionResult ViewDetails(string id)
        {
            var currentClass = this.classService.GetClassForDetails(id);

            return View(currentClass);
        }


        //public IActionResult ShowDetails()
        //{

        //}

    }
}
