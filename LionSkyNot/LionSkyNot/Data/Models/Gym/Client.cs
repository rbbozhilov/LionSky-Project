using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Gym;

namespace LionSkyNot.Data.Models.Gym
{
    public class Client
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        public int Number { get; set; }
    
        [Required]
        public DateTime ExpireDate { get; set; }

    }
}
