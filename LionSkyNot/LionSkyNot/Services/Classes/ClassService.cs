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

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var allTrainers = this.data.Trainers.Select(t => new TrainerViewModel()
            {
                Id = t.Id,
                FullName = t.FullName,
            }).ToList();

            return allTrainers;
        }
    }
}
