using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawyerServices.Common
{
    public class SubmitApplicationModel
    {
        [Required(ErrorMessage = "Material cost is required")]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Profesion { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Office { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public IFormFile ImgUrl { get; set; }
    }
}
