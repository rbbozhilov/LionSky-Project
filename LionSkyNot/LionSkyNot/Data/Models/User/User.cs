using LionSkyNot.Data.Models.Classes;
using LionSkyNot.Data.Models.Shop;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LionSkyNot.Data.Models.User
{
    public class User : IdentityUser
    {

        public User()
        {
            this.Classes = new HashSet<ClassUser>();
        }


        public ICollection<ClassUser> Classes { get; set; }

    }
}
