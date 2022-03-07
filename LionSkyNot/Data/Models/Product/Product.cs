using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LionSkyNot.Data.Models.Product
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }

        public Type Type { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }


    }
}
