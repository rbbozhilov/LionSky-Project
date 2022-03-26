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


        public IActionResult Index(IEnumerable<ClassViewModel> classModel)
        {

            classModel = this.classService.GetAllClasses();

            return View(classModel);
        }


        public IActionResult Buy(string id)
        {

            return RedirectToAction("Index");
        }

        //public IActionResult ShowDetails()
        //{

        //}

    }
}
