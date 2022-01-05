using LawyerServices.Data.Models.SystemModels;
using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Data.Models
{
    public class WorkingTimeDays : BaseDeletableModel<string>
    {
        [DataType(DataType.Time)]
        public DateTime StarFrom { get; set; }

        [DataType(DataType.Time)]
        public DateTime EndTo { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public bool IsDayOff { get; set; }

        public string WorkingTimeId { get; set; }

        public WorkingTime WorkingTime { get; set; }
    }
}
