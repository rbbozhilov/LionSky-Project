using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Categorie;


namespace LionSkyNot.Data.Models.Classes
{
    public class Categorie
    {
        
        public Categorie()
        {
            this.Trainers = new HashSet<Trainer>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(CategorieNameMaxLength)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Trainer> Trainers { get; set; }

    }
}
