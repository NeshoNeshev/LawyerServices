using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.UserModels
{
    public class ApplicationUserViewModel : IMapFrom<ApplicationUser>
    {
        public string? Id { get; init; }

        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        public string? Email { get; init; }

        public string? PhoneNumber { get; set; }

        public string? ImgUrl { get; set; }

        public bool IsSendSms { get; set; } 

        public bool IsReminderForComing { get; set; }

        public bool IsReserved { get; set; }

        public bool CountUp { get; set; }
    }
}
