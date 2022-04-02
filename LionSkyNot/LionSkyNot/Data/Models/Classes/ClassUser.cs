using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Data.Models.Classes
{
    public class ClassUser
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public LionSkyNot.Data.Models.User.User User { get; set; }

        public string ClassId { get; set; }

        public Class Class { get; set; }


    }
}
