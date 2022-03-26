using LionSkyNot.Views.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Products
{
    public class AddProductFormModel
    {

  
        [MaxLength(255)]
        [MinLength(2)]
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        [MinLength(10)]
        public string Description { get; set; }

        
        public decimal Price { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

        [Range(1,100)]
        public int InStock { get; set; }

        public int TypeId { get; set; }

        public IEnumerable<ProductTypeViewModel>? Type { get; set; }

        public int BrandId { get; set; }

        public IEnumerable<ProductBrandViewModel>? Brand { get; set; }
      
    }
}
