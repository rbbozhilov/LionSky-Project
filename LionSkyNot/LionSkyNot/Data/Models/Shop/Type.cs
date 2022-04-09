using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Type;


namespace LionSkyNot.Data.Models.Shop
{
    public class Type
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string TypeName { get; set; }


    }
}
