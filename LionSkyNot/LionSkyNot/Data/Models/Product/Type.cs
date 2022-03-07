using System.ComponentModel.DataAnnotations;
using static LionSkyNot.Data.DataConstants;

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
