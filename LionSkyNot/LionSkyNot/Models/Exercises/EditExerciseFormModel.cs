using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Exercises
{
    public class EditExerciseFormModel
    {

        [MaxLength(200)]
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
