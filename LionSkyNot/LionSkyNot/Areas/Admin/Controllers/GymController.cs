using LionSkyNot.Areas.Admin.Models.Gym;
using LionSkyNot.Controllers;
using LionSkyNot.Services.Gym;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static LionSkyNot.Areas.Admin.AdminConstants;

namespace LionSkyNot.Areas.Admin.Controllers
{

    [Area(AreaName)]
    [Authorize(Roles = ModeratorAndAdminRole)]
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
        public async Task<IActionResult> AddClient(ClientFormModel clientModel)
        {

            if (clientModel.StartDate > clientModel.ExpireDate)
            {
                this.ModelState.AddModelError("errorDate", "Cannot start date be after expire date");
            }


            if (!ModelState.IsValid)
            {
                return View(clientModel);
            }

            await this.clientService.CreateAsync(
                                       clientModel.FullName,
                                       clientModel.StartDate,
                                       clientModel.ExpireDate);

            return RedirectToAction("Successfull");
        }


        public IActionResult EditClient(int id)
        {

            var client = this.clientService.SearchByNumberAndName(id.ToString());

            if (client == null)
            {
                return BadRequest();
            }

            return View(new ClientFormModel()
            {
                FullName = client.FullName,
                StartDate = client.StartDate,
                ExpireDate = client.ExpireDate,
            });
        }


        [HttpPost]
        public async Task<IActionResult> EditClient(ClientFormModel clientModel, int id)
        {

            if (clientModel.StartDate > clientModel.ExpireDate)
            {
                this.ModelState.AddModelError("errorDate", "Cannot start date be after expire date");
            }

            if (!ModelState.IsValid)
            {
                return View(clientModel);
            }


            bool isEditted = await this.clientService.EditAsync(
                                                       id,
                                                       clientModel.FullName,
                                                       clientModel.StartDate,
                                                       clientModel.ExpireDate);

            if (!isEditted)
            {
                return BadRequest();
            }

            return RedirectToAction("Successfull");
        }


        public IActionResult SearchClient(string searchTerm)
        {

            if (!string.IsNullOrWhiteSpace(searchTerm) && !string.IsNullOrEmpty(searchTerm))
            {
                var searchClient = this.clientService.SearchByNumberAndName(searchTerm);

                return View("ShowClient", searchClient);

            }

            return View();

        }

        public IActionResult Successfull()
        => this.View();

    }
}
