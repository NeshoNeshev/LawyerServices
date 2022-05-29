using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.ProfileModels
{
    public class EditLawyerProfileModel
    {
        public string Id { get; set; }

        public string? userId { get; set; }

        [Required(ErrorMessage ="Имената за задълцителни")]
        [StringLength(40, ErrorMessage ="Името не може да е по дълго от 40 символа" )]
        public string? Names { get; set; }


        [Url(ErrorMessage ="Въведете валиден URL")]
        public string? WebSite { get; set; }

        [Required(ErrorMessage ="Адресът е задължителен")]
        [StringLength(60, ErrorMessage ="Адресът не може да е по дълъг от 60 символа")]
        public string? Address { get; set; }

        [Required(ErrorMessage ="Телефонът е задължителен")]
        [Phone(ErrorMessage ="Въведете валиден телефон")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage ="Имейлът е задължителен")]
        [EmailAddress(ErrorMessage ="Въведете валиден имейл")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Описанието на заглавната част задължително")]
        public string? HeaderText { get; set; }

        [Required(ErrorMessage = "Описанието на секция За Вас задължително")]
        public string? AboutText { get; set; }

        public bool IsPublicPhoneNuber { get; set; } = false;

        public bool IsOwnerPermision { get; set; }

        public bool IsRemainderForComming { get; set; }
    }
}
