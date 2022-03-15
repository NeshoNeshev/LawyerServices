using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.UserModels
{
    public class UserRequestModel
    {
      
        public string? UserId { get; set; }

        [Required]
        [StringLength(15)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(15)]
        public string? LastName { get; set; }

       
        public string? CompanyId { get; init; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? MoreInformation { get; set; }


        public string? WorkingTimeExceptionId { get; set; }
    }
}
