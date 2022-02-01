namespace LawyerServices.Common.WorkingTimeModels
{
    public class WorkingTimeExceptionViewModel
    {
        public DateTime StarFrom { get; set; }

        public DateTime EndTo { get; set; }

        public DateTime Date { get; set; }

        public string? AppointmentType { get; set; }
    }
}
