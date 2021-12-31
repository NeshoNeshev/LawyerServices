using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Common
{
    public class SubmitApplicationModel
    {
        [Required]
        [DisplayName("Въведете Име")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Въведете Фамилия")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Въведете имейл")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Въведете телефон")]
        public string PhoneNumber { get; set; }

        [Required]
        public string TownName { get; set; }

        [Required]
        public string Profesion { get; set; }
    }
}
