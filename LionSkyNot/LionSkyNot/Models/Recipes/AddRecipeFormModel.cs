using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Recipe
{
    public class AddRecipeFormModel
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public float Calories { get; set; }

        public float Protein { get; set; }

        public float Fat { get; set; }

        public float Carbohydrates { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }


    }
}
