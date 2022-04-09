using LionSkyNot.Views.ViewModels.Products;

using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Product;


namespace LionSkyNot.Models.Products
{
    public class AddProductFormModel
    {

  
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; }

        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

        [Range(MinInStock,MaxInStock)]
        public int InStock { get; set; }

        public int TypeId { get; set; }

        public IEnumerable<ProductTypeViewModel>? Type { get; set; }

        public int BrandId { get; set; }

        public IEnumerable<ProductBrandViewModel>? Brand { get; set; }
      
    }
}
