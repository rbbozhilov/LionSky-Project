using LionSkyNot.Models.Contact;

using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LionSkyNot.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IConfiguration configuration;

        public ContactController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var apiKey = this.configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("rasmuse3@abv.bg", model.Name);
            var subject = model.Subject;
            var to = new EmailAddress("rbbojilov@gmail.com");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = model.Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return View("SendSuccess");
        }

    }
}
