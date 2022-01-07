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
            this.Requests = new HashSet<RequestModel>();
        }

        public string? CompanyId { get; set; }

        public Company Company { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsBan { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<RequestModel> Requests { get; set; }
    }
}
