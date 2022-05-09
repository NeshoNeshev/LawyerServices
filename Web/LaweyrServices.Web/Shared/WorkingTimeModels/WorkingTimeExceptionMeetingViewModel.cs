using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.WorkingTimeModels
{
    public class WorkingTimeExceptionMeetingViewModel : IMapFrom<WorkingTimeException>
    {
        public string Id { get; set; }

        public DateTime StarFrom { get; set; }

        public string CaseNumber { get; set; }

        public string Court { get; set; }

        public string TypeOfCase { get; set; }


        public string SideCase { get; set; }

        public string MoreInformation { get; set; }
    }
}
