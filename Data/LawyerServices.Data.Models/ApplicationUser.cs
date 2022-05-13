using LawyerServices.Data.Models.SystemModels;
using Microsoft.AspNetCore.Identity;

namespace LawyerServices.Data.Models
{
    public class ApplicationUser : IdentityUser, IDateInfo, IDeletable
    {

        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.WorkingTimeExceptions = new HashSet<WorkingTimeException>();
            this.Reviews = new HashSet<Review>();
        }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? ImgUrl { get; set; }

        public string? CompanyId { get; set; }

        public Company Company { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsBan { get; set; }

        public byte CancelledCount { get; set; } = 0;

        public byte NotShowUpCount { get; set; } = 0;

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsSendSms { get; set; } = true;

        public bool IsReminderForComing { get; set; } = true;

        public bool IsReserved { get; set; } = true;

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<WorkingTimeException> WorkingTimeExceptions { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

    }
}
