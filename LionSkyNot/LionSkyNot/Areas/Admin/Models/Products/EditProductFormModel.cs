using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Product;


namespace LionSkyNot.Models.Products
{
    public class EditProductFormModel
    {

        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        [Required]
        public string Name { get; set; }

        [Range(MinPromotionPercentage,MaxPromotionPercentage)]
        public float PromotionPercentage { get; set; }

        [Range(MinPrice,MaxPrice)]
        public decimal Price { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

 
    }
}
