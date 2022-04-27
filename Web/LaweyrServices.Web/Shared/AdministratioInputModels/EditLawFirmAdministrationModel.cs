using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.AdministratioInputModels
{
    public class EditLawFirmAdministrationModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage ="Полето е задължително")]
        [StringLength(50, ErrorMessage ="Не по дълго от 50 знака")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(500, ErrorMessage = "Не по дълго от 500 знака")]
        public string? About { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Phone(ErrorMessage ="Въведете валиден телефон")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(50, ErrorMessage = "Не по дълго от 50 знака")]
        public string? Name { get; set; }

        public string? ImgUrl { get; set; }

        [Url]
        public string? WebSiteUrl { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [EmailAddress(ErrorMessage ="Въведете валиден имейл")]
        public string Email { get; set; }

        [Url]
        public string? FacebookUrl { get; set; }

        [Url]
        public string? LinkedinUrl { get; set; }
    }
}
