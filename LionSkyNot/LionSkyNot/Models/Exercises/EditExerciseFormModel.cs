using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Exercise;


namespace LionSkyNot.Models.Exercises
{
    public class EditExerciseFormModel
    {

        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        [Required]
        public string Name { get; set; }

        [Url]
        [Required]
        public string VideoUrl { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

    }
}
