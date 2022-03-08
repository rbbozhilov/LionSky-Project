using LionSkyNot.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LionSkyNot.Controllers
{
    public class HomeController : BaseController
    {
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminPanel()
        {
            return View();
        }


        public IActionResult AddProduct()
        {
            return View();
        }


        public IActionResult DeleteProduct()
        {
            return View();
        }

        public IActionResult AddRecipe()
        {
            return View();
        }

        public IActionResult DeleteRecipe()
        {
            return View();
        }

        public IActionResult AddClass()
        {
            return View();
        }

        public IActionResult DeleteClass()
        {
            return View();
        }

        public IActionResult GetCandidates()
        {
            return View();
        }

        public IActionResult AddExercise()
        {
            return View();
        }

        public IActionResult DeleteExercise()
        {
            return View();
        }

        public IActionResult FiredTrainer()
        {
            return View();
        }

        public IActionResult FiredCooker()
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