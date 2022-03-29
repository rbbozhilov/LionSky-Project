using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LionSkyNot.Data.DataConstants;

namespace LionSkyNot.Data.Models.Classes
{
    public class Trainer
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [MaxLength(255)]
        [Required]
        public string Description { get; set; }

        public int YearOfExperience { get; set; }

        public DateTime BirthDate { get; set; }

        public float Weight { get; set; }

        public float Height { get; set; }


        [ForeignKey(nameof(Categorie))]
        public int CategorieId { get; set; }

        public Categorie Categorie { get; set; }

        public string UserId { get; set; }

    }
}
