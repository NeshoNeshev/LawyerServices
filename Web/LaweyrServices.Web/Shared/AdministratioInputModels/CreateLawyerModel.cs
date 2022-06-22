using LawyerServices.Data.Models.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.AdministratioInputModels
{
    public class CreateLawyerModel
    {

        [Required]
        [StringLength(30, ErrorMessage = "Адресът не може да е по дълъг от 30 символа")]
        public string Names { get; set; }

        [Required(ErrorMessage = "Изберете град")]
        public string TownName { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Адресът не може да е по дълъг от 60 символа")]
        public string AddressLocation { get; set; }

        public string? OfficeName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }   

        [Required(ErrorMessage = "Ролята е задължителна")]
        public Profession Role { get; set; }

        public string? RequestId { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(2000, ErrorMessage = "Максимум 2000 символа")]
        public string? HeaderText { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(20000, ErrorMessage = "Максимум 20000 символа")]
        public string? AboutText { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Проведете разговор и верифицирайте")]
        public bool PhoneVerification { get; set; }

        public List<string>? Languages { get; set; }

        [Required(ErrorMessage = "Юрисдикцията е задължителна")]
        public string? Jurisdiction { get; set; }

        [Required(ErrorMessage = "Дата на лиценз е задължителна")]
        public string? LicenceDate { get; set; }

        public string? LastChecked { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public bool IsPublicPhoneNuber { get; set; }=false;

    }
}
