﻿namespace LawyerServices.Common.DateModels
{
    public class Appointment
    {
        public string? Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Text { get; set; }
    }
}
