using LionSkyNot.Views.ViewModels.Classes;

namespace LionSkyNot.Services.Classes
{
    public interface IClassService
    {

        IEnumerable<TrainerViewModel> GetAllTrainers();

        void Create(string fullname,string imageUrl,decimal price,int MaxPractitionerCount,int trainerId,DateTime startDateTime,DateTime endDateTime);

    }
}
