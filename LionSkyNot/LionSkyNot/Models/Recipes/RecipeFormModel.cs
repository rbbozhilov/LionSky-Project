using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Recipe
{
    public class RecipeFormModel
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

    }
}
