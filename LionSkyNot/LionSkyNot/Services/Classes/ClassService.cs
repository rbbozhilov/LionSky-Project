using LionSkyNot.Data;
using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Models.Class;
using LionSkyNot.Views.ViewModels.Classes;

namespace LionSkyNot.Services.Classes
{
    public class ClassService : IClassService
    {

        private LionSkyDbContext data;

        public ClassService(LionSkyDbContext data)
        {
            this.data = data;
        }


        public void Create(string className, string imageUrl, decimal price, int maxPractitionerCount, int trainerId, DateTime startDateTime, DateTime endDateTime)
        {
            var @class = new Class()
            {
                ClassName = className,
                ImageUrl = imageUrl,
                Price = price,
                MaxPractitionerCount = maxPractitionerCount,
                TrainerId = trainerId,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime
            };


            this.data.Classes.Add(@class);

            this.data.SaveChanges();
        }


        public bool Edit(
                         string id,
                         string className,
                         string imageUrl,
                         int maxPractitionerCount,
                         decimal price,
                         DateTime startDate,
                         DateTime endDate,
                         int trainerId
                         )
        {


            var currentClass = this.data.Classes.Where(c => c.Id == id && c.IsDeleted == false)
                                                .FirstOrDefault();

            if (currentClass == null)
            {
                return false;
            }


            currentClass.ClassName = className;
            currentClass.ImageUrl = imageUrl;
            currentClass.Price = price;
            currentClass.StartDateTime = startDate;
            currentClass.EndDateTime = endDate;
            currentClass.MaxPractitionerCount = maxPractitionerCount;
            currentClass.TrainerId = trainerId;


            this.data.SaveChanges();

            return true;

        }


        public bool Delete(string id)
        {
            var currentClass = this.data.Classes.Where(c => c.Id == id && c.IsDeleted == false)
                                                .FirstOrDefault();

            if (currentClass == null)
            {
                return false;
            }

            currentClass.IsDeleted = true;

            this.data.SaveChanges();

            return true;
        }


        public IEnumerable<TrainerClassViewModel> GetAllTrainers()
        {
            var allTrainers = this.data.Trainers
                                       .Select(t => new TrainerClassViewModel()
                                       {
                                           Id = t.Id,
                                           FullName = t.FullName,
                                       }).ToList();

            return allTrainers;
        }


        public IEnumerable<ClassTrainerViewModel> GetAllTrainerClasses(int trainerId)
         => this.data.Classes
                     .Where(c => c.TrainerId == trainerId)
                     .Select(t => new ClassTrainerViewModel()
                     {
                         PractitionerCount = t.PractitionerCount,
                         Price = t.Price,
                         StartDateTime = t.StartDateTime.ToString(),
                         EndDateTime = t.EndDateTime.ToString(),
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

                         StartDateTime = c.StartDateTime.ToString(),
                         EndDateTime = c.EndDateTime.ToString(),
                         ImageUrl = c.ImageUrl,
                         Trainer = c.Trainer.FullName,
                         Id = c.Id
                     })
                     .ToList();


        public IEnumerable<ClassFormModelForAdmin> GetAllClassesForAdmin()
         => this.data.Classes.Where(c => c.IsDeleted == false)
                             .Select(c => new ClassFormModelForAdmin()
                             {
                                 ClassName = c.ClassName,
                                 Id = c.Id,
                             })
                             .ToList();


        public ClassFormModel GetClassById(string id)
         => this.data.Classes.Where(c => c.Id == id && c.IsDeleted == false)
                             .Select(c => new ClassFormModel()
                             {
                                 ClassName = c.ClassName,
                                 StartDateTime = c.StartDateTime,
                                 EndDateTime = c.EndDateTime,
                                 ImageUrl = c.ImageUrl,
                                 MaxPractitionerCount = c.MaxPractitionerCount,
                                 Price = c.Price,
                                 TrainerId = c.Trainer.Id,
                             })
                             .FirstOrDefault();

        public ClassFormModel GetClassById(string id, string className, string imageUrl, int maxPractitionerCount, decimal price, DateTime startDate, DateTime endDate, int trainerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClassViewModel> GetAllFitnessClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Fitness")
                    .Select(c => new ClassViewModel()
                    {
                        Id = c.Id,
                        ClassName = c.ClassName,
                        ImageUrl = c.ImageUrl,
                        Trainer = c.Trainer.FullName,
                        StartDateTime = c.StartDateTime.ToString(),
                        EndDateTime = c.EndDateTime.ToString()
                    })
                    .ToList();

        public IEnumerable<ClassViewModel> GetAllYogaClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Yoga")
                    .Select(c => new ClassViewModel()
                    {
                        Id = c.Id,
                        ClassName = c.ClassName,
                        ImageUrl = c.ImageUrl,
                        Trainer = c.Trainer.FullName,
                        StartDateTime = c.StartDateTime.ToString(),
                        EndDateTime = c.EndDateTime.ToString()
                    })
                    .ToList();

        public IEnumerable<ClassViewModel> GetAllMmaClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Mma")
                    .Select(c => new ClassViewModel()
                    {
                        Id = c.Id,
                        ClassName = c.ClassName,
                        ImageUrl = c.ImageUrl,
                        Trainer = c.Trainer.FullName,
                        StartDateTime = c.StartDateTime.ToString(),
                        EndDateTime = c.EndDateTime.ToString()
                    })
                    .ToList();

        public IEnumerable<ClassViewModel> GetAllBoxClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Box")
                    .Select(c => new ClassViewModel()
                    {
                        Id = c.Id,
                        ClassName = c.ClassName,
                        ImageUrl = c.ImageUrl,
                        Trainer = c.Trainer.FullName,
                        StartDateTime = c.StartDateTime.ToString(),
                        EndDateTime = c.EndDateTime.ToString()
                    })
                    .ToList();

        public IEnumerable<ClassViewModel> GetAllWrestlingClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Wrestling")
                    .Select(c => new ClassViewModel()
                    {
                        Id = c.Id,
                        ClassName = c.ClassName,
                        ImageUrl = c.ImageUrl,
                        Trainer = c.Trainer.FullName,
                        StartDateTime = c.StartDateTime.ToString(),
                        EndDateTime = c.EndDateTime.ToString()
                    })
                    .ToList();

        public IEnumerable<ClassViewModel> GetAllAthleticClass()
        => this.data.Classes
                    .Where(c => c.Trainer.Categorie.Name == "Athletic")
                    .Select(c => new ClassViewModel()
                    {
                        Id = c.Id,
                        ClassName = c.ClassName,
                        ImageUrl = c.ImageUrl,
                        Trainer = c.Trainer.FullName,
                        StartDateTime = c.StartDateTime.ToString(),
                        EndDateTime = c.EndDateTime.ToString()
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
        => this.data.Classes.Where(c => c.Trainer.Categorie.Name == "Yoga").Count();


        private int GetCountOfFitnessClass()
        => this.data.Classes.Where(c => c.Trainer.Categorie.Name == "Fitness").Count();


        private int GetCountOfMmaClass()
        => this.data.Classes.Where(c => c.Trainer.Categorie.Name == "Mma").Count();


        private int GetCountOfBoxClass()
        => this.data.Classes.Where(c => c.Trainer.Categorie.Name == "Box").Count();


        private int GetCountOfWrestlingClass()
        => this.data.Classes.Where(c => c.Trainer.Categorie.Name == "Wrestling").Count();


        private int GetCountOfAthleticClass()
        => this.data.Classes.Where(c => c.Trainer.Categorie.Name == "Athletic").Count();

    }
}
