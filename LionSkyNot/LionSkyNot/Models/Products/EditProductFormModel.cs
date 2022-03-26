using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Products
{
    public class EditProductFormModel
    {

        [MaxLength(255)]
        [MinLength(2)]
        [Required]
        public string Name { get; set; }


        public decimal Price { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

 

    }
}
