using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawyerServices.Common
{
    public class SubmitApplicationModel
    {
        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        public string Names { get; set; }

        [Required]
        public string Profession { get; set; }

        [Required(ErrorMessage = "Адресът е задължителен")]
        public string Address { get; set; }

        [Required]
        public string Town { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен")]
        [EmailAddress]
        public string Email { get; set; }

        public string Office { get; set; }

        [Required(ErrorMessage = "Телефонът е задължителен")]
        [Phone]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public IFormFile ImgUrl { get; set; }
    }
}
