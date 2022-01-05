using LawyerServices.Data.Models.SystemModels;
using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Data.Models
{
    public class WorkingTimeException : BaseDeletableModel<string>
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        //Todo : ExeptionType enum

        [DataType(DataType.Time)]
        public DateTime StarFrom { get; set; }

        [DataType(DataType.Time)]
        public DateTime EndTo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string WorkingTimeId { get; set; }

        public WorkingTime WorkingTime { get; set; }
    }
}
