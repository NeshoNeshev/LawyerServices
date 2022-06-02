using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.WorkingTimeModels
{
    public class WorkingTimeExceptionEmailModel : IMapFrom<WorkingTimeException>
    {
        public DateTime StarFrom { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public WorkingTimeUserViewModel WorkingTime { get; set; }
    }
}
