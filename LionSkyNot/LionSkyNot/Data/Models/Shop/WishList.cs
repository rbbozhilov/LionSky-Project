using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using LionSkyNot.Data.Models.Classes;


namespace LionSkyNot.Data.Models.Shop
{
    public class WishList 
    {

        public WishList()
        {
            this.Products = new HashSet<Product>();
            this.Classes = new HashSet<Class>();
        }


        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public string UserId { get; set; }

    }
}
