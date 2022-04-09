using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Payment;


namespace LionSkyNot.Models.Products
{
    public class PaymentFormModel
    {

        [RegularExpression(CardNumberRegularExpression, ErrorMessage = CardNumberErrorMessage)]
        [Required]
        public string CardNumber { get; set; }


        [RegularExpression(MonthYearCardRegularExpression, ErrorMessage = MonthYearCardErrorMessage)]
        [Required]
        public string MonthYearOfCard { get; set; }

        [RegularExpression(CvvCodeRegularExpression, ErrorMessage = CvvCodeErrorMessage)]
        [Required]
        public int CvvCode { get; set; }


        [MaxLength(NameOnCardMaxLength, ErrorMessage = NameOnCardMaxLengthErrorMessage)]
        [MinLength(NameOnCardMinLength, ErrorMessage = NameOnCardMinLengthErrorMessage)]
        [Required]
        public string NameOnCard { get; set; }


    }
}
