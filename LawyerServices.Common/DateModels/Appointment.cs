using LawyerServices.Common.Enumerations;

namespace LawyerServices.Common.DateModels
{
    public class Appointment
    {
        public string? Id { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public string? Text { get; set; }

        public string? CaseNumber { get; set; }

        public string? Court { get; set; }

        public string? MoreInformation { get; set; }

        public TimeSpan? StartDiapazone { get; set; }

        public TimeSpan? EndtDiapazone { get; set; }

        public int? Step { get; set; }

        public DateTime? Date { get; set; }

        public bool? IsChecked { get; set; }
    }
}
