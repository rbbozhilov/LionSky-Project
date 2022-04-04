using LionSkyNot.Models.Calculator;
using LionSkyNot.Views.ViewModels.Calculator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{

    [Authorize]
    public class CalculatorController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CalculateFormModel calculateModel, CalculatorViewModel model)
        {

            if (!this.ModelState.IsValid)
            {

                return View(calculateModel);

            }

            var calculator = new Calculator();
            model = calculator.Calculation(calculateModel.Goal, calculateModel.Weight);


            return View("Result", model);
        }


    }
}
