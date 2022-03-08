using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    public class ContactController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }


    }
}
