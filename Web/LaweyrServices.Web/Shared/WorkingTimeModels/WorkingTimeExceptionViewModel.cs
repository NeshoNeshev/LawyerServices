using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.WorkingTimeModels
{
    public class WorkingTimeExceptionViewModel : IMapFrom<WorkingTimeException>
    {
        public string Id { get; set; }

        public DateTime StarFrom { get; set; }

        public DateTime EndTo { get; set; }

        public DateTime Date { get; set; }

        public string? AppointmentType { get; set; }
    }
}
