using LionSkyNot.Data;
using LionSkyNot.Data.Models.Classes;

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

        public IEnumerable<TrainerClassViewModel> GetAllTrainers()
        {
            var allTrainers = this.data.Trainers.Select(t => new TrainerClassViewModel()
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
                     .Select(c => new ClassViewModel()
                     {
                         ClassName = c.ClassName,
                         Price = c.Price,
                         StartDateTime = c.StartDateTime.ToString(),
                         EndDateTime = c.EndDateTime.ToString(),
                         Categorie = c.Trainer.Categorie.Name,
                         ImageUrl = c.ImageUrl,
                         Trainer = c.Trainer.FullName,
                         Id = c.Id
                     })
                     .ToList();


    }
}
