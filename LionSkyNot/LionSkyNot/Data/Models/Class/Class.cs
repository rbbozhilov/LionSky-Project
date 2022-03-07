using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LionSkyNot.Data.Models.Class
{
    public class Class
    {

        [Key]
        public int Id { get; set; }

        public int PractitionerCount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        [ForeignKey(nameof(Trainer))]
        public int TrainerId { get; set; }

        public Trainer Trainer { get; set; }

        [ForeignKey(nameof(Categorie))]
        public int CategorieId { get; set; }

        public Categorie Categorie { get; set; }


    }
}
