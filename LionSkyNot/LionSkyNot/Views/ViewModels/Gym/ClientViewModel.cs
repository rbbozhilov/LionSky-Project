using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Views.ViewModels.Gym
{
    public class ClientViewModel
    {

        public string FullName { get; set; }

        public int Number { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpireDate { get; set; }

    }
}
