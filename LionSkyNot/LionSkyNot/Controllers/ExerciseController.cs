using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class ExerciseController : BaseController 
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
