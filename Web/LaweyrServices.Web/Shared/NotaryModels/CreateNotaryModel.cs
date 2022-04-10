using LawyerServices.Data.Models.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.NotaryModels
{
    public class CreateNotaryModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "Адресът не може да е по дълъг от 30 символа")]
        public string Names { get; set; }

        [Required(ErrorMessage = "Изберете град")]
        public string TownName { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Адресът не може да е по дълъг от 40 символа")]
        public string AddressLocation { get; set; }

        public string? OfficeName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ролята е задължителна")]
        public Profession Role { get; set; }

        public string? RequestId { get; set; }

        [Url(ErrorMessage ="Въведете валиден Url")]
        public string? WebSite { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "описанието не може да е по дълъго от 200 символа")]
        public string About { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Проведете разговор и верифицирайте")]
        public bool PhoneVerification { get; set; }
    }
}
