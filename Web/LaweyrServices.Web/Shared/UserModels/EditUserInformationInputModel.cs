using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.UserModels
{
    public class EditUserInformationInputModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage ="Полето е задължително")]
        [StringLength(15,ErrorMessage = "Името не може да е по дълго от 15 символа")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [StringLength(15, ErrorMessage = "Името не може да е по дълго от 15 символа")]
        public string? LastName { get; set; }

        //[Required(ErrorMessage = "Полето е задължително")]
        //[EmailAddress(ErrorMessage ="Въведете валиден имейл")]
        //public string? Email { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Phone(ErrorMessage ="Въведете валиден телефон")]
        public string? PhoneNumber { get; set; }

        public bool IsSendSms { get; set; }

        public bool IsReminderForComing { get; set; }

        public bool IsReserved { get; set; }
    }
}
