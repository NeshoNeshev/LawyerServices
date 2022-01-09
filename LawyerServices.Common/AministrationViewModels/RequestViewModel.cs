using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.AministrationViewModels
{
    public class RequestViewModel : IMapFrom<Request>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Profesion { get; set; }

        public string Address { get; set; }

        public string Town { get; set; }

        public string Email { get; set; }

        public string Office { get; set; }

        public string PhoneNumber { get; set; }

        public bool? IsApproved { get; set; }
    }
}
