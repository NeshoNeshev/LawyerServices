using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.WorkingTimeModels
{
    public class WorkingTimeExceptionUserViewModel : IMapFrom<WorkingTimeException>
    {

        public string Id { get; set; }

        public DateTime StarFrom { get; set; }

        public bool IsApproved { get; set; }

        public bool IsCanceled { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? Email { get; set; }

        public string? UserFirstName { get; set; }

        public WorkingTimeUserViewModel WorkingTime { get; set; }
    }
}
