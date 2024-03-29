﻿using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.AministrationViewModels
{
    public class RequestViewModel : IMapFrom<Request>
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Profession { get; set; }

        public string Address { get; set; }

        public string Town { get; set; }

        public string Email { get; set; }

        public string Office { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsApproved { get; set; }

        public bool AcceptedTermOfUse { get; set; }

    }
}
