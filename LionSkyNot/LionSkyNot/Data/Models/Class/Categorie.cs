using System.ComponentModel.DataAnnotations;
using static LionSkyNot.Data.DataConstants;

namespace LionSkyNot.Data.Models.Class
{
    public class Categorie
    {
        
        public Categorie()
        {
            this.Trainers = new HashSet<Trainer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Trainer> Trainers { get; set; }

    }
}
