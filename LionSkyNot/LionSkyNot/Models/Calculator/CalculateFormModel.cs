using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Calculator;

namespace LionSkyNot.Models.Calculator
{
    public class CalculateFormModel
    {

        [Required]
        [Range(MinAge,MaxAge , ErrorMessage = AgeErrorMessage)]
        public int Age { get; set; }

        [Required]
        [Range(MinWeight,MaxWeight,ErrorMessage = WeightErrorMessage)]
        public float Weight { get; set; }
        
        [Required]
        [Range(MinHeight, MaxHeight, ErrorMessage = HeightErrorMessage)]
        public float Height { get; set; }

        public string Goal { get; set; }

    }
}
