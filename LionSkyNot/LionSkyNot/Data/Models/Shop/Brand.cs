using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Brand;


namespace LionSkyNot.Data.Models.Shop
{
    public class Brand
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string BrandName { get; set; }

    }
}
