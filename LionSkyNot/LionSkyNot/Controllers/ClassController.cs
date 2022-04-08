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
        => this.classService = classService;



        public IActionResult Index(AllClassViewModel allClassModel)
        => View(this.classService.GetCountOfAllClasses());


        [Authorize]
        public IActionResult ViewAllFitnessClass()
        => View(this.classService.GetAllClassesByCategorieName("Fitness"));


        [Authorize]
        public IActionResult ViewAllYogaClass()
        => View(this.classService.GetAllClassesByCategorieName("Yoga"));


        [Authorize]
        public IActionResult ViewAllMmaClass()
        => View(this.classService.GetAllClassesByCategorieName("Mma"));


        [Authorize]
        public IActionResult ViewAllBoxClass()
        => View(this.classService.GetAllClassesByCategorieName("Box"));


        [Authorize]
        public IActionResult ViewAllWrestlingClass()
        => View(this.classService.GetAllClassesByCategorieName("Wrestling"));


        [Authorize]
        public IActionResult ViewAllAthleticClass()
        => View(this.classService.GetAllClassesByCategorieName("Athletic"));


        [Authorize]
        public IActionResult ViewDetails(string id)
        => View(this.classService.GetClassForDetails(id));


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

            return View(this.classService.GetUserClasses(currentUserId));
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
