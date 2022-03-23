using LionSkyNot.Data;
using LionSkyNot.Infrastructure;

namespace LionSkyNot.Services.Users
{
    public class UserService : IUserService
    {

        private readonly LionSkyDbContext data;

        public UserService(LionSkyDbContext data)
        {
            this.data = data;
        }


        public bool ContainsUsername(string username)
        => this.data.Users.Any(x => x.UserName == username);

    }
}
