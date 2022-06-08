using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared
{
    public class SubmitApplicationModel
    {
        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        public string Names { get; set; }


        [Required(ErrorMessage = "Адресът е задължителен")]
        public string Address { get; set; }

        [Required]
        public string Town { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Телефонът е задължителен")]
        [Phone]
        public string PhoneNumber { get; set; }

    }
}
