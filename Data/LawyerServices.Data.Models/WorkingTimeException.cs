using LawyerServices.Data.Models.SystemModels;
using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Data.Models
{
    public class WorkingTimeException : BaseDeletableModel<string>
    {
        
        [DataType(DataType.Time)]
        public DateTime StarFrom { get; set; }

        [DataType(DataType.Time)]
        public DateTime EndTo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string? AppointmentType { get; set; }

        public string? CaseNumber { get; set; }

        public string? Court { get; set; }

        public string? TypeOfCase { get; set; }

        public string? SideCase { get; set; }

        public string? MoreInformation { get; set; }

        public string WorkingTimeId { get; set; }

        public WorkingTime WorkingTime { get; set; }

        public bool IsCanceled { get; set; } = false;

        public string? ReasonFromCanceled { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsRequested { get; set; } = false;

        public bool NotShowUp { get; set; } = false;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? UserId { get; set; }

        public ApplicationUser? User { get; set; }
    }
}