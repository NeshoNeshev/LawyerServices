using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.AdministratioInputModels
{
    public class EditLawyerModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage ="Полето е задължително")]
        [StringLength(50, ErrorMessage ="Името не може да е по дълго от 50 символа")]
        public string? Names { get; set; }

        [Url(ErrorMessage ="Въведете валиден Url")]
        public string? WebSite { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(50, ErrorMessage = "Адресът не може да е по дълго от 50 символа")]
        public string? Address { get; set; }

        public string? OfficeName { get; set; }

        public string? Languages { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(2000, ErrorMessage = "Максимум 2000 символа")]
        public string? HeaderText { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(20000, ErrorMessage = "Максимум 20000 символа")]
        public string? AboutText { get; set; }

        public string? ImgUrl { get; set; }

        [Required(ErrorMessage = "Юрисдикцията е задължителна")]
        public string? Jurisdiction { get; set; }

        [Required(ErrorMessage = "Дата на лиценз е задължителна")]
        public string? LicenceDate { get; set; }

        public string? LastChecked { get; set; }
    }
}
