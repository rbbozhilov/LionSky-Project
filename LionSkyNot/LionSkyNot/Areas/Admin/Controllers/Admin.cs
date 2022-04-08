using LionSkyNot.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Areas.Admin.Controllers
{
    [Area(AdminConstants.AreaName)]
    public class Admin : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
