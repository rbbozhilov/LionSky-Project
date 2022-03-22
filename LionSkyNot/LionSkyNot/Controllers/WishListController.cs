using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LionSkyNot.Controllers
{
    [Authorize]
    public class WishListController : BaseController
    {

        public IActionResult Index()
        {

            return View();
        }

    }
}
