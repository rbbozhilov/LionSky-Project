using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.Contact;


namespace LionSkyNot.Models.Contact
{
    public class ContactFormModel
    {

        [Required]
        [Display(Name = DisplayName)]
        [MinLength(ContactNameMinLength, ErrorMessage = NameMinLengthErrorMessage)]
        [MaxLength(ContactNameMaxLength, ErrorMessage = NameMaxLengthErrorMessage)]
        public string Name { get; set; }

        [Required]
        [StringLength(StringLengthSubject, ErrorMessage = SubjectStringLengthErrorMessage)]
        public string Subject { get; set; }

        [Required]
        [Display(Name = DisplayMessage)]
        [StringLength(StringLengthMessage, ErrorMessage = SubjectStringLengthErrorMessage)]
        public string Message { get; set; }


    }
}
