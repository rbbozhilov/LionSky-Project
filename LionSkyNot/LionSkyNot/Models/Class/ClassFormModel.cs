using System.ComponentModel.DataAnnotations;

using LionSkyNot.Views.ViewModels.Classes;

using static LionSkyNot.Data.DataConstants.Class;


namespace LionSkyNot.Models.Class
{
    public class ClassFormModel
    {

        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        [Required]
        public string ClassName { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

        [Range(MinPractitionerCount, MaxPractitionerCounts)]
        public int MaxPractitionerCount { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int TrainerId { get; set; }

        public IEnumerable<TrainerClassViewModel>? Trainers { get; set; }

    }
}
