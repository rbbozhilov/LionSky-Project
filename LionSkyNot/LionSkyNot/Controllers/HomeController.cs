using LionSkyNot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LionSkyNot.Controllers
{
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Class()
        {
            return View();
        }

        public IActionResult Exercise()
        {
            return View();
        }

        public IActionResult Recipe()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View("NotFound");
        }

        public IActionResult Calculator()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}