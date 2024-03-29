﻿using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.FixedCostModels;
using LaweyrServices.Web.Shared.RatingModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.LawyerViewModels
{
    public class LawyerListItem : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string AreaName { get; set; }

        public ICollection<AreasCompanyViewModel> AreasCompanies { get; set; }

        public ICollection<FixedCostViewModel> FixedCostServices { get; set; }

        public string WebSite { get; set; }

        public string Address { get; set; }

        public string OfficeName { get; set; }

        public string Languages { get; set; }

        public string? HeaderText { get; set; }

        public string? AboutText { get; set; }

        public string? ImgUrl { get; set; }

        public string TownName { get; set; }

        public bool FreeFirstAppointment { get; set; } 

        public bool NoWinNoFee { get; set; } 

        public bool FixedCost { get; set; }

        public bool MeetingClientLocation { get; set; }

        public string? LawFirmName { get; set; }

        public string? LawFirmId { get; set; }

        public string? Jurisdiction { get; set; }

        public string? LicenceDate { get; set; }

        public string? LastChecked { get; set; }

        public string? EarlyTime { get; set; }

        public int ReviewsCount { get; set; }

        public double AverageGrade { get; set; }
        public IEnumerable<RatingsViewModel> Reviews { get; set; }

        public WorkingTimeViewModel WorkingTime { get; set; }
    }
}
