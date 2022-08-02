using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class Gym : BaseController
    {

        public IActionResult Index()
        => View();

    }
}
