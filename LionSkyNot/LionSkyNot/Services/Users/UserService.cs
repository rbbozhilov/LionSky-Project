using LionSkyNot.Data;
using LionSkyNot.Data.Models.User;

namespace LionSkyNot.Services.Users
{
    public class UserService : IUserService
    {

        private LionSkyDbContext data;


        public UserService(LionSkyDbContext data)
        {
            this.data = data;
        }


        public User GetUser(string userName)
        => this.data.Users.FirstOrDefault(u => u.UserName == userName);

    }
}
