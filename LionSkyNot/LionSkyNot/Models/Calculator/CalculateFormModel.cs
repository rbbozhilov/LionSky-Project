using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Calculator
{
    public class CalculateFormModel
    {

        [Required]
        [Range(1,120 , ErrorMessage = "Enter again , age is not correct")]
        public int Age { get; set; }

        [Required]
        public float Weight { get; set; }
        
        [Required]
        public float Height { get; set; }

        public string Goal { get; set; }

    }
}
