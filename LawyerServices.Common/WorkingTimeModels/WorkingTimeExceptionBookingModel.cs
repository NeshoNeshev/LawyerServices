using LawyerServices.Common.UserModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.WorkingTimeModels
{
    public class WorkingTimeExceptionBookingModel : IMapFrom<WorkingTimeException>
    {
        public string Id { get; set; }

        public DateTime StarFrom { get; set; }

        public bool IsApproved { get; set; } 

        public bool IsRequested { get; set; }

        public ApplicationUserViewModel User { get; set; }
    }
}
