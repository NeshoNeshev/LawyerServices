using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.WorkingTimeModels
{
    public class WorkingTimeExceptionBookingModel : IMapFrom<WorkingTimeException>
    {
        public string Id { get; set; }

        public DateTime StarFrom { get; set; }

        public bool IsApproved { get; set; } 

        public bool IsRequested { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public ApplicationUserViewModel User { get; set; }
    }
}
