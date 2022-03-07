using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Data.Models.Product
{
    public class Type
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string TypeName { get; set; }


    }
}
