using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.ContactModels
{
    public class ContactInputModel
    {
        [Required(ErrorMessage = "Полето е задължително.")]
        public string Names { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [EmailAddress(ErrorMessage = "Въведете валиден имейл.")]
        public string  Email { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Phone(ErrorMessage ="Въведете валиден телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Съобщението е задължително.")]
        [StringLength(500, ErrorMessage ="Съобщението не може да е по дълго от 500 символа")]
        public string Content { get; set; }
    }
}
