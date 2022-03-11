using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.DateModels
{
    public class AppointmentViewModel : IMapFrom<WorkingTimeException>
    {
        public DateTime StarFrom { get; set; }

        public DateTime EndTo { get; set; }
    }
}
