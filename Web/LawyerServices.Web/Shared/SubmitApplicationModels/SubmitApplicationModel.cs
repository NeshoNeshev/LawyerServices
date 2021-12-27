using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LawyerServices.Web.Shared.SubmitApplicationModels
{
    public class SubmitApplicationModel
    {
        [BindProperty]
        [DisplayName("Въведете Име")]
        public string FirstName { get; set; }

        [BindProperty]
        [DisplayName("Въведете Фамилия")]
        public string LastName { get; set; }

        [BindProperty]
        [DisplayName("Въведете имейл")]
        public string Email { get; set; }

        [BindProperty]
        [DisplayName("Въведете телефон")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [DisplayName("Въведете web site")]
        public string WebSite { get; set; }

        [BindProperty]
        [DisplayName("Въведете име на кантора")]
        public string OfficeName { get; set; }

        [BindProperty]
        public string TownName { get; set; }
    }
}
