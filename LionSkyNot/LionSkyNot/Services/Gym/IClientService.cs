using LionSkyNot.Areas.Admin.Models.Gym;
using LionSkyNot.Views.ViewModels.Gym;

namespace LionSkyNot.Services.Gym
{
    public interface IClientService
    {

        void Create(
                   string fullName,
                   DateTime startDate,
                   DateTime expireDate);

        bool Edit(
                 int number,
                 string fullName,
                 DateTime StartDate,
                 DateTime ExpireDate
                );

        bool CheckNumber(int number);

        ClientFormModel GetClientByNumber(int number);

        ClientViewModel SearchByNumberAndName(string searchTerm);
       
    }
}
