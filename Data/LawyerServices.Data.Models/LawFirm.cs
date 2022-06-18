using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class LawFirm : BaseDeletableModel<string>
    {

        public LawFirm()
        {
            this.Reviews = new HashSet<Review>();
            this.Companies = new HashSet<Company>();
            this.Moderators = new HashSet<Moderator>();
        }

        public string? Name { get; set; }

        public string TownId { get; set; }

        public virtual Town Town { get; set; }

        public string? Address { get; set; }

        public string? About { get; set; }

        public string? PhoneNumbers { get; set; }

        public bool IsPublicPhoneNuber { get; set; } = false;

        public string? ImgUrl { get; set; }

        public string? WebSiteUrl { get; set; }

        public string Email { get; set; }

        public string? FacebookUrl { get; set; }

        public string? LinkedinUrl { get; set; }

        public bool PhoneVerification { get; set; } = false;

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<Moderator> Moderators { get; set; }
    }
}
