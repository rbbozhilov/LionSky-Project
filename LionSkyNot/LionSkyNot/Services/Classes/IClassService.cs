using LionSkyNot.Models.Class;
using LionSkyNot.Views.ViewModels.Classes;

namespace LionSkyNot.Services.Classes
{
    public interface IClassService
    {

        void Create(string fullname,
           string imageUrl,
           decimal price,
           int MaxPractitionerCount,
           int trainerId,
           DateTime startDateTime,
           DateTime endDateTime);

        bool Edit(
                  string id,
                  string className,
                  string imageUrl,
                  int maxPractitionerCount,
                  decimal price,
                  DateTime startDate,
                  DateTime endDate,
                  int trainerId
                  );


        bool Delete(string id);

        //[MaxLength(255)]
        //[Required]
        //public string ClassName { get; set; }

        //[Url]
        //[Required]
        //public string ImageUrl { get; set; }

        //public int MaxPractitionerCount { get; set; }

        //public decimal Price { get; set; }

        //public DateTime StartDateTime { get; set; }

        //public DateTime EndDateTime { get; set; }

        //public int TrainerId { get; set; }


        ClassFormModel GetClassById(string id);

        IEnumerable<ClassFormModelForAdmin> GetAllClassesForAdmin();

        IEnumerable<ClassViewModel> GetAllClasses();

        IEnumerable<TrainerClassViewModel> GetAllTrainers();

        IEnumerable<ClassTrainerViewModel> GetAllTrainerClasses(int trainerId);


    }
}
