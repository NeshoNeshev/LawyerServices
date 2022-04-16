using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.AdministratioInputModels
{
    public class EditNotaryModel
    {

        public string Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage ="Името не може да е по дълго от 20 символа")]
        public string Names { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Адресът не може да е по дълго от 40 символа")]
        public string AddressLocation { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Името не може да е по дълго от 20 символа")]
        public string? OfficeName { get; set; }

        [Required]
        [Url]
        public string? WebSite { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Описанието не може да е по дълго от 20 символа")]
        public string About { get; set; }
    }
}
