using LionSkyNot.Data.Models.User;

namespace LionSkyNot.Services.Users
{
    public interface IUserService
    {

        User GetUser(string userName);

    }
}
