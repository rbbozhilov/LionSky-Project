using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Calculator
{
    public class CalculateFormModel
    {

        [Required]
        [Range(1,120 , ErrorMessage = "Enter again , age is not correct")]
        public int Age { get; set; }

        [Required]
        [Range(1,150,ErrorMessage = "Enter again, weight should be between {1} and {2}")]
        public float Weight { get; set; }
        
        [Required]
        [Range(1, 220, ErrorMessage = "Enter again, height should be between {1} and {2}")]
        public float Height { get; set; }

        public string Goal { get; set; }

    }
}
