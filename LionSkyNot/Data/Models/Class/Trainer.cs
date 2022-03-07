using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Data.Models.Class
{
    public class Trainer
    {

        public Trainer()
        {
            this.Categories = new HashSet<Categorie>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string FullName { get; set; }

        public int YearOfExperience { get; set; }

        public DateTime? BirthDate { get; set; }

        public float Weight { get; set; }

        public float Height { get; set; }

        public virtual ICollection<Categorie> Categories { get; set; }


    }
}
