﻿namespace LaweyrServices.Web.Shared.FixedCostModels
{
    public class FeaturesInputModel
    {
        public bool FreeFirstAppointment { get; set; }

        public bool NoWinNoFee { get; set; }

        public bool FixedCost { get; set; }

        public bool MeetingClientLocation { get; set; }

        public string? LawyerId { get; set; }
    }
}