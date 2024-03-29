﻿using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.WorkingTimeModels
{
    public class WorkingTimeExceptionBookingModel : IMapFrom<WorkingTimeException>
    {
        public string Id { get; set; }

        public DateTime StarFrom { get; set; }

        public bool IsApproved { get; set; } 

        public bool IsRequested { get; set; }

        public bool NotShowUp { get; set; } = false;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? AppointmentType { get; set; }

        public string? Email { get; set; }

        public string? MoreInformation { get; set; }

        public string? CaseNumber { get; set; }

        public string? Court { get; set; }

        public string? TypeOfCase { get; set; }

        public string? SideCase { get; set; }

        public ApplicationUserViewModel? User { get; set; }
    }
}
