using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class ShopController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
