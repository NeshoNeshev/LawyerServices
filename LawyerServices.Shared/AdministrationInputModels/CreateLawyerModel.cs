using LawyerServices.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Shared.AdministrationInputModels
{
    public class CreateLawyerModel
    {

        [Required]
        [StringLength(20, ErrorMessage = "")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "")]
        public string LastName { get; set; }
        
        [Required]
        public Town TownName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "")]
        public string AddressLocation { get; set; }

        public string OfficeName { get; set; }

        [Url]
        public string WebSite { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
