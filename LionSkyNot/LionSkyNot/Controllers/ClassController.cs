using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class ClassController : BaseController 
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
