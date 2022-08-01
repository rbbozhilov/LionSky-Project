using LionSkyNot.Areas.Admin.Models.Gym;
using LionSkyNot.Controllers;
using LionSkyNot.Services.Gym;
using Microsoft.AspNetCore.Mvc;

using static LionSkyNot.Areas.Admin.AdminConstants;

namespace LionSkyNot.Areas.Admin.Controllers
{

    [Area(AreaName)]
    public class GymController : BaseController
    {

        private IClientService clientService;


        public GymController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public IActionResult AddClient()
        => this.View();


        [HttpPost]
        public IActionResult AddClient(ClientFormModel clientModel)
        {

            if (clientModel.StartDate > clientModel.ExpireDate)
            {
                this.ModelState.AddModelError("errorDate", "Cannot start date be after expire date");
            }


            if (!ModelState.IsValid)
            {
                return View(clientModel);
            }

            this.clientService.Create(
                                      clientModel.FullName,
                                      clientModel.StartDate,
                                      clientModel.ExpireDate);

            return RedirectToAction("Successfull");
        }


        public IActionResult Successfull()
        => this.View();

    }
}
