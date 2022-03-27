using LionSkyNot.Views.ViewModels.Classes;
using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Class
{
    public class ClassFormModel
    {

        [MaxLength(255)]
        [Required]
        public string ClassName { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }

        public int MaxPractitionerCount { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int TrainerId { get; set; }

        public IEnumerable<TrainerClassViewModel>? Trainers { get; set; }

    }
}
