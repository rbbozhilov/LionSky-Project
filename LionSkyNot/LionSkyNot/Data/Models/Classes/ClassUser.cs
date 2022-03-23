namespace LionSkyNot.Data.Models.Classes
{
    public class ClassUser
    {

        public string UserId { get; set; }

        public LionSkyNot.Data.Models.User.User User { get; set; }

        public string ClassId { get; set; }

        public Class Class { get; set; }


    }
}
