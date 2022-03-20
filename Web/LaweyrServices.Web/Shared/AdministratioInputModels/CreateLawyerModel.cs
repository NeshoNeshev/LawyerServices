using LawyerServices.Data.Models.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.AdministratioInputModels
{
    public class CreateLawyerModel
    {

        [Required]
        [StringLength(20, ErrorMessage = "")]
        public string Names { get; set; }

        [Required]
        public string TownName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "")]
        public string AddressLocation { get; set; }

        public string OfficeName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }   

        [Required]
        public Profession Role { get; set; }

        public string RequestId { get; set; }
    }
}
