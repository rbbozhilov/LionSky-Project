using LionSkyNot.Infrastructure;
using LionSkyNot.Services.Classes;

using LionSkyNot.Views.ViewModels.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LionSkyNot.Controllers
{
    public class ClassController : BaseController
    {

        private IClassService classService;

        public ClassController(IClassService classService)
        {
            this.classService = classService;
        }


        public IActionResult Index(AllClassViewModel allClassModel)
        {

            allClassModel = this.classService.GetCountOfAllClasses();

            return View(allClassModel);
        }

        [Authorize]
        public IActionResult ViewAllFitnessClass()
        {

            var allFitnessClasses = this.classService.GetAllClassesByCategorieName("Fitness");

            return View(allFitnessClasses);
        }

        [Authorize]
        public IActionResult ViewAllYogaClass()
        {
            var allYogaClasses = this.classService.GetAllClassesByCategorieName("Yoga");

            return View(allYogaClasses);
        }

        [Authorize]
        public IActionResult ViewAllMmaClass()
        {
            var allMmaClass = this.classService.GetAllClassesByCategorieName("Mma");

            return View(allMmaClass);
        }

        [Authorize]
        public IActionResult ViewAllBoxClass()
        {
            var allBoxClass = this.classService.GetAllClassesByCategorieName("Box");

            return View(allBoxClass);
        }

        [Authorize]
        public IActionResult ViewAllWrestlingClass()
        {
            var allWrestlingClass = this.classService.GetAllClassesByCategorieName("Wrestling");

            return View(allWrestlingClass);
        }

        [Authorize]
        public IActionResult ViewAllAthleticClass()
        {
            var allAthleticClass = this.classService.GetAllClassesByCategorieName("Athletic");

            return View(allAthleticClass);
        }

        [Authorize]
        public IActionResult ViewDetails(string id)
        {
            var currentClass = this.classService.GetClassForDetails(id);

            return View(currentClass);
        }

        [Authorize]
        public IActionResult Join(string id)
        {

            var currentUserId = ClaimsPrincipalExtensions.GetId(this.User);
 
            if (this.classService.CheckFreePlace(id))
            {
                return View("ToMuchPeopleInClass");
            }

            if (!this.classService.AddUserToClass(currentUserId, id))
            {
                return View("UserAlreadyInClass");
            }

            return View("SuccessJoin");
        }

        public IActionResult ViewUserClasses()
        {
            var currentUserId = ClaimsPrincipalExtensions.GetId(this.User);

            var userClasses = this.classService.GetUserClasses(currentUserId);

            return View(userClasses);
        }

        public IActionResult RemoveFromClassUser(string id)
        {

            var currentUserId = ClaimsPrincipalExtensions.GetId(this.User);

            if (!this.classService.RemovingClassFromUser(currentUserId, id))
            {
                return BadRequest();
            }

            return View("SuccessRemoveClassUser");
        }
    }
}
