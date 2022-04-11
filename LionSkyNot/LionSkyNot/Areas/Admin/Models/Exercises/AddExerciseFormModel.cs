using System.ComponentModel.DataAnnotations;

using LionSkyNot.Views.ViewModels.Exercises;

using static LionSkyNot.Data.DataConstants.Exercise;


namespace LionSkyNot.Models.Exercises
{
    public class AddExerciseFormModel
    {
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        [Required]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        [Required]
        public string Description { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

        [Display(Name = DisplayTypeName)]
        public int TypeId { get; set; }

        public IEnumerable<TypeExerciseViewModel>? Type { get; set; }


    }
}
