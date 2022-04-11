using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Recipe;


namespace LionSkyNot.Models.Recipe
{
    public class RecipeFormModel
    {

        [Required]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

    }
}
