using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class RecipeController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
