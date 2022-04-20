using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.ProfileModels
{
    public class EditLawyerProfileModel
    {
        public string Id { get; set; }

        public string? userId { get; set; }

        [Required]
        public string? Names { get; set; }

        [Required]
        [Url]
        public string? WebSite { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
