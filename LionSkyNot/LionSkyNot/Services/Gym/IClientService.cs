using LionSkyNot.Views.ViewModels.Gym;

namespace LionSkyNot.Services.Gym
{
    public interface IClientService
    {

        void Create(
                   string fullName,
                   DateTime startDate,
                   DateTime expireDate);

        bool CheckNumber(int number);

        ClientViewModel SearchByNumberAndName(string searchTerm);
       
    }
}
