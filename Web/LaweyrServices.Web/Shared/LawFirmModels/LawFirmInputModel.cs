using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.LawFirmModels
{
    public class LawFirmInputModel
    {
        [Required(ErrorMessage ="Полето е задължително")]
        [StringLength(50, ErrorMessage = "Името не може дае по дълго от 20 символа")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(60, ErrorMessage = "Адресът не може да е по дълъг от 60 символа")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(1600, ErrorMessage = "Описанието не може да е по дълго от 1600 символа")]
        public string? About { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [EmailAddress(ErrorMessage ="Въведете валиден имейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Phone(ErrorMessage = "Въведете валиден телефонен номер")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public bool PhoneVerification { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public bool IsPublicPhone { get; set; }
    }
}
