using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Data.Models.Product
{
    public class Brand
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string BrandName { get; set; }




    }
}
