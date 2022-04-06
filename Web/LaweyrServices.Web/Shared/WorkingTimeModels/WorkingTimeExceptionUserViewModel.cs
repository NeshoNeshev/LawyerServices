using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.WorkingTimeModels
{
    public class WorkingTimeExceptionUserViewModel : IMapFrom<WorkingTimeException>
    {

        public string Id { get; set; }

        public DateTime StarFrom { get; set; }

        public bool IsApproved { get; set; }

        public WorkingTimeUserViewModel WorkingTime { get; set; }
    }
}
