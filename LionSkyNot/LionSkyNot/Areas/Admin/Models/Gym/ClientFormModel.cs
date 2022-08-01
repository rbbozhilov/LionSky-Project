using System.ComponentModel.DataAnnotations;
using static LionSkyNot.Data.DataConstants.Gym;

namespace LionSkyNot.Areas.Admin.Models.Gym
{
    public class ClientFormModel
    {

        [Required]
        [MaxLength(FullNameMaxLength)]
        [MinLength(FullNameMinLength)]
        public string FullName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

    }
}
