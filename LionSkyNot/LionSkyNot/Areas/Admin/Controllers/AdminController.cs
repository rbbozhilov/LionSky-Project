using LionSkyNot.Controllers;

using Microsoft.AspNetCore.Mvc;

using static LionSkyNot.Areas.Admin.AdminConstants;


namespace LionSkyNot.Areas.Admin.Controllers
{
    [Area(AreaName)]
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
