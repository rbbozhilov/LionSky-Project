using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class GymController : BaseController
    {

        public IActionResult Index()
        => View();

    }
}
