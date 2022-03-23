using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace LionSkyNot.Data.Models.Shop
{
    public class WishList 
    {

        public WishList()
        {
            this.Products = new HashSet<Product>();
        }


        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public virtual ICollection<Product> Products { get; set; }


        public string UserId { get; set; }

    
    }
}
