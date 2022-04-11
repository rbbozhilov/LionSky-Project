using System.ComponentModel.DataAnnotations;

using LionSkyNot.Models.Trainers;


namespace LionSkyNot.Views.ViewModels.Trainers
{
    public class AllTrainersViewModel
    {


        public IEnumerable<TrainerListViewModel> Trainers { get; set; }
        
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public IEnumerable<TrainerSort> Sort { get; set; }


    }
}
