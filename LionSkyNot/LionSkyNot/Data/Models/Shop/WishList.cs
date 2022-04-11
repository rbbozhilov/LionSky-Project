using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LionSkyNot.Data.Models.Shop
{
    public class WishList 
    {

        public WishList()
        {
            this.Products = new HashSet<WishListsProducts>();
        }


        [Key]
        public int Id { get; set; }


        public string UserId { get; set; }

        public virtual ICollection<WishListsProducts> Products { get; set; }


    }
}
