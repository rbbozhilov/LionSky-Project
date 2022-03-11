using LionSkyNot.Views.ViewModels.Exercises;
using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Exercises
{
    public class AddExerciseFormModel
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

        [Display(Name = "Type")]
        public int TypeId { get; set; }

        public IEnumerable<TypeExerciseViewModel>? Type { get; set; }


    }
}
