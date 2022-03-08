using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Models.Contact
{
    public class ContactFormModel
    {

        [Required]
        [Display(Name = "Name")]
        [MinLength(2, ErrorMessage = "The name must be at least 2 letters")]
        [MaxLength(20, ErrorMessage = "Too long name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Too long subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        [StringLength(5000, ErrorMessage = "Too long message")]
        public string Message { get; set; }


    }
}
