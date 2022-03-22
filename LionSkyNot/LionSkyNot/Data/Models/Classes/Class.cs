using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LionSkyNot.Data.DataConstants;

namespace LionSkyNot.Data.Models.Classes
{
    public class Class
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string ClassName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int PractitionerCount { get; set; }

        public int MaxPractitionerCount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        [ForeignKey(nameof(Trainer))]
        public int TrainerId { get; set; }

        public Trainer Trainer { get; set; }

        public bool IsDeleted { get; set; } = false;

     
    }
}
