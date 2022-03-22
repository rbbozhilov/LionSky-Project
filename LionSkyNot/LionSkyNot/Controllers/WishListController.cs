using LionSkyNot.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{
    [Authorize]
    public class WishListController : BaseController
    {

        private LionSkyDbContext data;

        public WishListController(LionSkyDbContext data)
        {
            this.data = data;
        }


        public IActionResult Index()
        {
            var user = Infrastructure.ClaimsPrincipalExtensions.GetId(this.User);


            return View();
        }

    }
}
