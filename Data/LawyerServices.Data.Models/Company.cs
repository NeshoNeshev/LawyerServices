using LawyerServices.Data.Models.Enumerations;
using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Company : BaseDeletableModel<string>
    {
        public Company()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.AreasCompanies = new HashSet<AreasCompany>();
            this.FixedCostServices = new HashSet<FixedCostService>();
            this.Reviews = new HashSet<Review>();
        }

        public string Names { get; set; }

        public string? WebSite { get; set; }

        public string Address { get; set; }

        public string? OfficeName { get; set; }

        public bool WorkInSaturday { get; set; } = false;

        public bool WorkInSunday { get; set; } = false;

        public string? Languages { get; set; }

        public string? HeaderText { get; set; }

        public string? AboutText { get; set; }

        public string? Jurisdiction { get; set; }

        public string? LicenceDate { get; set; }

        public string? LastChecked { get; set; }

        public int? YearFirstAdmitted { get; set; }

        public bool FreeFirstAppointment { get; set; } = false;

        public bool NoWinNoFee { get; set; } = false;

        public bool FixedCost { get; set; } = false;

        public bool MeetingClientLocation { get; set; } = false;

        public bool PhoneVerification { get; set; } = false;

        public Profession Profession { get; set; }

        public string TownId { get; set; }

        public virtual Town Town { get; set; }

        public string? LawFirmId { get; set; }

        public virtual LawFirm LawFirm { get; set; }

        public string WorkingTimeId { get; set; }

        public WorkingTime WorkingTime { get; set; }

        public bool IsOwner { get; set; } = false;

        public bool IsOwnerPermision { get; set; } = false;

        public string? PhoneNumbers { get; set; }

        public string? OfficeEmails { get; set; }

        public bool IsPublicPhoneNuber { get; set; } = false;

        public bool StopAccount { get; set; }

        public bool IsSendSms { get; set; } = false;

        public bool IsReminderForComing { get; set; } = false;

        public string? RequestId { get; set; }

        public Request Request { get; set; }

        public string? ImgUrl { get; set; }

        public virtual ICollection<AreasCompany> AreasCompanies { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<FixedCostService> FixedCostServices { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
