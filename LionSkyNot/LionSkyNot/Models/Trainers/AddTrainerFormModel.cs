using LionSkyNot.Views.ViewModels.Trainers;
using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models
{
    public class AddTrainerFormModel
    {

        [MaxLength(50)]
        [Required]
        public string FullName { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

        [MaxLength(255)]
        [Required]
        public string Description { get; set; }

        public int YearOfExperience { get; set; }

        public DateTime BirthDate { get; set; }

        public float Weight { get; set; }

        public float Height { get; set; }

        public int CategorieId { get; set; }

        public IEnumerable<CategorieViewModel>? Categorie { get; set; }



    }
}
