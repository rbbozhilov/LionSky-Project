using LionSkyNot.Views.ViewModels.Trainers;

using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Trainer;


namespace LionSkyNot.Models.Trainers
{
    public class AddTrainerFromAdminFormModel
    {
        
        [Required]
        public string Username { get; set; }

        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        [Required]
        public string FullName { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

        [MaxLength(DescriptionMaxLength)]
        [MinLength(DescriptionMinLength)]
        [Required]
        public string Description { get; set; }

        [Range(YearOfExperienceMin, YearOfExperienceMax)]
        public int YearOfExperience { get; set; }

        public DateTime BirthDate { get; set; }

        [Range(WeightMin, WeightMax)]
        public float Weight { get; set; }

        [Range(HeightMin, HeightMax)]
        public float Height { get; set; }

        public int CategorieId { get; set; }

        public IEnumerable<CategorieViewModel>? Categorie { get; set; }

    }
}
