using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.UserModels
{
    public class UserRequestModel
    {
      
        public string? UserId { get; set; }

        [Required(ErrorMessage ="Името е задължително")]
        [StringLength(15, ErrorMessage ="Името не може да е по дълго от 15 символа")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна")]
        [StringLength(15, ErrorMessage = "Фамилията не може да е повече от 15 символа")]
        public string? LastName { get; set; }

       
        public string? CompanyId { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен")]
        [EmailAddress(ErrorMessage ="Въведете валиден имейл")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Телефонът е задължителен")]
        [Phone(ErrorMessage = "Въведете валиден телефонен номер")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string? MoreInformation { get; set; }

        public bool IsReminderForComing { get; set; }
        public string? WorkingTimeExceptionId { get; set; }
    }
}
