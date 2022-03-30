using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Products
{
    public class EditProductFormModel
    {

        [MaxLength(255)]
        [MinLength(2)]
        [Required]
        public string Name { get; set; }

        [Range(0,100)]
        public float PromotionPercentage { get; set; }

        [Range(0,1000)]
        public decimal Price { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

 
    }
}
