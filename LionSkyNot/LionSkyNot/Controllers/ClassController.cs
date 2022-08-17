using LionSkyNot.Infrastructure;

using LionSkyNot.Services.Classes;

using LionSkyNot.Views.ViewModels.Classes;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Caching.Memory;


namespace LionSkyNot.Controllers
{
    public class ClassController : BaseController
    {

        private IClassService classService;
        private IMemoryCache cache;

        public ClassController(IClassService classService, IMemoryCache cache)
        {
            this.classService = classService;
            this.cache = cache;
        }


        public IActionResult Index(AllClassViewModel allClassModel)
        {

            const string allCountOfClassesKeyCache = "AllCountOfClassesKeyCache";

            allClassModel = this.cache.Get<AllClassViewModel>(allCountOfClassesKeyCache);

            if (allClassModel == null)
            {
                allClassModel = this.classService.GetCountOfAllClasses();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(allCountOfClassesKeyCache, allClassModel);
            }


            return View(allClassModel);
        }


        [Authorize]
        public IActionResult ViewAllFitnessClass()
        {

            const string allFitnessClassesKeyCache = "AllFitnessClassesKeyCache";

            var allFitnessClasses = this.cache.Get<IEnumerable<ClassViewModel>>(allFitnessClassesKeyCache);

            if (allFitnessClasses == null)
            {
                allFitnessClasses = this.classService.GetAllClassesByCategorieName("Fitness");

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(allFitnessClassesKeyCache, allFitnessClasses);
            }


            return View(allFitnessClasses);
        }


        [Authorize]
        public IActionResult ViewAllYogaClass()
        {

            const string allYogaClassesKeyCache = "AllYogaClassesKeyCache";

            var allYogaClasses = this.cache.Get<IEnumerable<ClassViewModel>>(allYogaClassesKeyCache);

            if (allYogaClasses == null)
            {
                allYogaClasses = this.classService.GetAllClassesByCategorieName("Yoga");

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(allYogaClassesKeyCache, allYogaClasses);
            }


            return View(allYogaClasses);
        }


        [Authorize]
        public IActionResult ViewAllMmaClass()
        {

            const string allMmaClassesKeyCache = "AllMmaClassesKeyCache";

            var allMmaClasses = this.cache.Get<IEnumerable<ClassViewModel>>(allMmaClassesKeyCache);

            if (allMmaClasses == null)
            {
                allMmaClasses = this.classService.GetAllClassesByCategorieName("Mma");

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(allMmaClassesKeyCache, allMmaClasses);
            }


            return View(allMmaClasses);
        }


        [Authorize]
        public IActionResult ViewAllBoxClass()
        {

            const string allBoxClassesKeyCache = "AllBoxClassesKeyCache";

            var allBoxClasses = this.cache.Get<IEnumerable<ClassViewModel>>(allBoxClassesKeyCache);

            if (allBoxClasses == null)
            {
                allBoxClasses = this.classService.GetAllClassesByCategorieName("Box");

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(allBoxClassesKeyCache, allBoxClasses);
            }
            return View(allBoxClasses);
        }


        [Authorize]
        public IActionResult ViewAllWrestlingClass()
        {

            const string allWrestlingClassesKeyCache = "AllWrestlingClassesKeyCache";

            var allWrestlingClasses = this.cache.Get<IEnumerable<ClassViewModel>>(allWrestlingClassesKeyCache);

            if (allWrestlingClasses == null)
            {
                allWrestlingClasses = this.classService.GetAllClassesByCategorieName("Wrestling");

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(allWrestlingClassesKeyCache, allWrestlingClasses);
            }
            return View(allWrestlingClasses);
        }


        [Authorize]
        public IActionResult ViewAllAthleticClass()
        {

            const string allAthleticClassesKeyCache = "AllAthleticClassesKeyCache";

            var allAthleticClasses = this.cache.Get<IEnumerable<ClassViewModel>>(allAthleticClassesKeyCache);

            if (allAthleticClasses == null)
            {
                allAthleticClasses = this.classService.GetAllClassesByCategorieName("Athletic");

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(allAthleticClassesKeyCache, allAthleticClasses);
            }
            return View(allAthleticClasses);
        }


        [Authorize]
        public IActionResult ViewDetails(string id)
        {

            const string viewDetailsKeyCache = "viewDetailsKeyCache";

            var viewDetails = this.cache.Get<ClassDetailsViewModel>(viewDetailsKeyCache);

            if (viewDetails == null)
            {
                viewDetails = this.classService.GetClassForDetails(id);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


                this.cache.Set(viewDetailsKeyCache, viewDetails);
            }
            return View(viewDetails);
        }


        [Authorize]
        public async Task<IActionResult> Join(string id)
        {

            var currentUserId = ClaimsPrincipalExtensions.GetId(this.User);

            if (this.classService.CheckFreePlace(id))
            {
                return View("ToMuchPeopleInClass");
            }

            var result = await this.classService.AddUserToClassAsync(currentUserId, id);

            if (!result)
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


        public async Task<IActionResult> RemoveFromClassUser(string id)
        {
            var currentUserId = ClaimsPrincipalExtensions.GetId(this.User);

            var result = await this.classService.RemovingClassFromUserAsync(currentUserId, id);

            if (!result)
            {
                return BadRequest();
            }
            return View("SuccessRemoveClassUser");
        }
    }
}
