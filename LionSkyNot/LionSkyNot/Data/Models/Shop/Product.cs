using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static LionSkyNot.Data.DataConstants.Product;


namespace LionSkyNot.Data.Models.Shop
{
    public class Product
    {

        public Product()
        {
            this.WishLists = new HashSet<WishListsProducts>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int CountInStock { get; set; }

        public int CountOfBuys { get; set; }

        [Column(TypeName = TypeDecimal)]
        public decimal Price { get; set; }

        [Column(TypeName = TypeDecimal)]
        public decimal PriceOnPromotion { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }

        public Type Type { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsOnPromotion { get; set; } = false;

        public virtual ICollection<WishListsProducts> WishLists { get; set; }

    }
}
