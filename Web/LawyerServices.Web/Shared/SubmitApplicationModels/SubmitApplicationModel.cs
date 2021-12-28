using System.ComponentModel;

namespace LawyerServices.Web.Shared.SubmitApplicationModels
{
    public class SubmitApplicationModel
    {
         //Todo: Binding?
        [DisplayName("Въведете Име")]
        public string FirstName { get; set; }

        [DisplayName("Въведете Фамилия")]
        public string LastName { get; set; }


        [DisplayName("Въведете имейл")]
        public string Email { get; set; }

    
        [DisplayName("Въведете телефон")]
        public string PhoneNumber { get; set; }

  
        public string TownName { get; set; }

  
        public string Profesion { get; set; }
    }
}
