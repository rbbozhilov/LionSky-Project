using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Products
{
    public class PaymentFormModel
    {

        [RegularExpression(@"^4[0-9]{12}(?:[0-9]{3})?$", ErrorMessage = "Invalid card number")]
        [Required]
        public string CardNumber { get; set; }


        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$",ErrorMessage = "Invalid date on card")]
        [Required]
        public string MonthYearOfCard { get; set; }

        [RegularExpression(@"^[0-9]{3,4}$",ErrorMessage = "Invalid cvv code")]
        [Required]
        public int CvvCode { get; set; }


        [MaxLength(30,ErrorMessage ="The name cannot be more then 30 characters")]
        [MinLength(3,ErrorMessage ="The name must be at least 3 characters")]
        [Required]
        public string NameOnCard { get; set; }


    }
}
