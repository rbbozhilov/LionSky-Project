using System.ComponentModel.DataAnnotations;
using static LionSkyNot.Data.DataConstants;

namespace LionSkyNot.Data.Models.Recipe
{
    public class Recipe
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
