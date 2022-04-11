using LionSkyNot.Models.Contact;

using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{
    public class ContactController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Index(ContactFormModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View("SendSuccess");
        }

    }
}
