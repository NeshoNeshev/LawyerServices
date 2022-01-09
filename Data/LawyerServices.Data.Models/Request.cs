using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Request : BaseDeletableModel<string>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string Profesion { get; set; }

        public string Address { get; set; }

        public string Town { get; set; }

        public string Email { get; set; }

        public string? Office { get; set; }

        public string PhoneNumber { get; set; }

        public bool? IsApproved { get; set; } = false;
    }
}
