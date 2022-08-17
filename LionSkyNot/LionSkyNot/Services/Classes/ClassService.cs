using LionSkyNot.Data;
using LionSkyNot.Data.Models.Classes;

using LionSkyNot.Models.Class;

using LionSkyNot.Views.ViewModels.Classes;

using Microsoft.EntityFrameworkCore;


namespace LionSkyNot.Services.Classes
{
    public class ClassService : IClassService
    {

        private LionSkyDbContext data;

        public ClassService(LionSkyDbContext data)
        {
            this.data = data;
        }



        public async Task CreateAsync(
                          string className,
                          string imageUrl,
                          int maxPractitionerCount,
                          int trainerId,
                          DateTime startDateTime,
                          DateTime endDateTime)
        {
            var @class = new Class()
            {
                ClassName = className,
                ImageUrl = imageUrl,
                MaxPractitionerCount = maxPractitionerCount,
                TrainerId = trainerId,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime
            };


            await this.data.Classes.AddAsync(@class);

            await this.data.SaveChangesAsync();
        }


        public async Task<bool> AddUserToClassAsync(string userId, string classId)
        {


            var currentClassUser = this.data.ClassUsers
                                            .Where(c => c.UserId == userId && c.ClassId == classId)
                                            .FirstOrDefault();

            if (currentClassUser != null)
            {
                return false;
            }



            var newClassUser = new ClassUser()
            {
                UserId = userId,
                ClassId = classId
            };

            var @class = this.data.Classes
                                  .Where(c => c.Id == classId)
                                  .FirstOrDefault();

            @class.PractitionerCount++;


            await this.data.ClassUsers.AddAsync(newClassUser);

            await this.data.SaveChangesAsync();

            return true;

        }


        public bool IsHaveTrainerById(int id)
        => this.data.Trainers.Any(t => t.Id == id);


        public bool CheckFreePlace(string classId)
        {
            var currentCount = this.data.Classes
                                        .Where(c => c.Id == classId)
                                        .Select(c => new
                                        {
                                            c.MaxPractitionerCount,
                                            c.PractitionerCount
                                        })
                                        .FirstOrDefault();

            return currentCount.PractitionerCount == currentCount.MaxPractitionerCount;

        }


        public bool IsUserHaveClasses(string userId)
        => this.data.ClassUsers
                    .Any(c => c.UserId == userId && c.Class.IsDeleted == false);


        public async Task<bool> EditAsync(
                         string id,
                         string className,
                         string imageUrl,
                         int maxPractitionerCount,
                         DateTime startDate,
                         DateTime endDate,
                         int trainerId)
        {


            var currentClass = this.data.Classes
                                        .Where(c => c.Id == id && c.IsDeleted == false)
                                        .FirstOrDefault();

            if (currentClass == null)
            {
                return false;
            }


            currentClass.ClassName = className;
            currentClass.ImageUrl = imageUrl;
            currentClass.StartDateTime = startDate;
            currentClass.EndDateTime = endDate;
            currentClass.MaxPractitionerCount = maxPractitionerCount;
            currentClass.TrainerId = trainerId;


            await this.data.SaveChangesAsync();

            return true;

        }


        public async Task<bool> DeleteAsync(string id)
        {
            var currentClass = this.data.Classes
                                        .Where(c => c.Id == id && c.IsDeleted == false)
                                        .FirstOrDefault();

            if (currentClass == null)
            {
                return false;
            }

            currentClass.IsDeleted = true;

            await this.data.SaveChangesAsync();

            return true;
        }


        public async Task<bool> RemovingClassFromUserAsync(string userId, string classId)
        {

            var currentClass = this.data.ClassUsers
                                        .Where(c => c.UserId == userId && c.ClassId == classId)
                                        .FirstOrDefault();

            if (currentClass == null)
            {
                return false;
            }

            this.data.ClassUsers.Remove(currentClass);

            var @class = this.data.Classes
                                .Where(c => c.Id == classId)
                                .FirstOrDefault();

            @class.PractitionerCount--;

            await this.data.SaveChangesAsync();

            return true;
        }


        public IEnumerable<ClassViewModel> GetUserClasses(string userId)
        => this.data.ClassUsers
                    .Include(c => c.Class)
                    .Where(c => c.UserId == userId && c.Class.IsDeleted == false)
                    .Select(c => new ClassViewModel()
                    {
                        Id = c.ClassId,
                        ClassName = c.Class.ClassName,
                        ImageUrl = c.Class.ImageUrl,
                        StartDateTime = c.Class.StartDateTime,
                        EndDateTime = c.Class.EndDateTime,
                        Trainer = c.Class.Trainer.FullName,
                        isDeletedTrainer = c.Class.Trainer.IsDeleted
                    })
                    .ToList();


        public IEnumerable<TrainerClassViewModel> GetAllTrainers()
        => this.data.Trainers
                    .Where(t => t.IsCandidate == false && t.IsDeleted == false)
                    .Select(t => new TrainerClassViewModel()
                    {
                        Id = t.Id,
                        FullName = t.FullName,
                    }).ToList();


        public IEnumerable<ClassTrainerViewModel> GetAllTrainerClasses(int trainerId)
         => this.data.Classes
                     .Where(c => c.TrainerId == trainerId && c.IsDeleted == false)
                     .Select(t => new ClassTrainerViewModel()
                     {
                         PractitionerCount = t.PractitionerCount,
                         StartDateTime = t.StartDateTime,
                         EndDateTime = t.EndDateTime,
                         Trainer = t.Trainer.FullName,
                         Categorie = t.Trainer.Categorie.Name,
                         ClassName = t.ClassName,
                         ImageUrl = t.ImageUrl
                     })
                     .ToList();


        public IEnumerable<ClassViewModel> GetAllClasses()
         => this.data.Classes
                     .Where(c => c.IsDeleted == false)
                     .Select(c => new ClassViewModel()
                     {
                         ClassName = c.ClassName,
                         StartDateTime = c.StartDateTime,
                         EndDateTime = c.EndDateTime,
                         ImageUrl = c.ImageUrl,
                         Trainer = c.Trainer.FullName,
                         Id = c.Id,
                         isDeletedTrainer = c.Trainer.IsDeleted
                     })
                     .ToList();


        public IEnumerable<ClassFormModelForAdmin> GetAllClassesForAdmin()
         => this.data.Classes
                     .Where(c => c.IsDeleted == false)
                     .Select(c => new ClassFormModelForAdmin()
                     {
                         ClassName = c.ClassName,
                         Id = c.Id,
                     })
                     .ToList();


        public ClassFormModel GetClassById(string id)
         => this.data.Classes
                     .Where(c => c.Id == id && c.IsDeleted == false)
                     .Select(c => new ClassFormModel()
                     {
                         ClassName = c.ClassName,
                         StartDateTime = c.StartDateTime,
                         EndDateTime = c.EndDateTime,
                         ImageUrl = c.ImageUrl,
                         MaxPractitionerCount = c.MaxPractitionerCount,
                         TrainerId = c.Trainer.Id,
                     })
                     .FirstOrDefault();


        public ClassDetailsViewModel GetClassForDetails(string id)
         => this.data.Classes
                     .Where(c => c.Id == id && c.IsDeleted == false)
                     .Select(c => new ClassDetailsViewModel()
                     {
                         ClassName = c.ClassName,
                         StartDateTime = c.StartDateTime,
                         EndDateTime = c.EndDateTime,
                         ImageUrl = c.ImageUrl,
                         MaxPractitionerCount = c.MaxPractitionerCount,
                         PractitionerCount = c.PractitionerCount,
                         TrainerName = c.Trainer.FullName,
                         IsDeletedTrainer = c.Trainer.IsDeleted
                     })
                     .FirstOrDefault();


        public IEnumerable<ClassViewModel> GetAllClassesByCategorieName(string categorieName)
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == categorieName && c.IsDeleted == false)
                    .Select(c => new ClassViewModel()
                    {
                        Id = c.Id,
                        ClassName = c.ClassName,
                        ImageUrl = c.ImageUrl,
                        Trainer = c.Trainer.FullName,
                        StartDateTime = c.StartDateTime,
                        EndDateTime = c.EndDateTime,
                        isDeletedTrainer = c.Trainer.IsDeleted
                    })
                    .ToList();


        public AllClassViewModel GetCountOfAllClasses()
        => new AllClassViewModel()
        {
            CountOfAthleticClass = this.GetCountOfAthleticClass(),
            CountOfMmaClass = this.GetCountOfMmaClass(),
            CountOfBoxClass = this.GetCountOfBoxClass(),
            CountOfFitnessClass = this.GetCountOfFitnessClass(),
            CountOfWrestlingClass = this.GetCountOfWrestlingClass(),
            CountOfYogaClass = this.GetCountOfYogaClass()
        };


        private int GetCountOfYogaClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Yoga" && c.IsDeleted == false)
                    .Count();


        private int GetCountOfFitnessClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Fitness" && c.IsDeleted == false)
                    .Count();


        private int GetCountOfMmaClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Mma" && c.IsDeleted == false)
                    .Count();


        private int GetCountOfBoxClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Box" && c.IsDeleted == false)
                    .Count();


        private int GetCountOfWrestlingClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Wrestling" && c.IsDeleted == false)
                    .Count();


        private int GetCountOfAthleticClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Athletic" && c.IsDeleted == false)
                    .Count();
    }
}