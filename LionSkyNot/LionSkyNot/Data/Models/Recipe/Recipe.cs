using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Recipe;


namespace LionSkyNot.Data.Models.Recipe
{
    public class Recipe
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
