using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class LawFirm : BaseDeletableModel<string>
    {

        public LawFirm()
        {
            this.Reviews = new HashSet<Review>();
            this.Companies = new HashSet<Company>();
        }

        public string? Name { get; set; }

        public string TownId { get; set; }

        public virtual Town Town { get; set; }

        public string? Address { get; set; }

        public string? About { get; set; }

        public string? PhoneNumber { get; set; }

        public string? ImgUrl { get; set; }

        public string? WebSiteUrl { get; set; }

        public string? FacebookUrl { get; set; }

        public string? LinkedinUrl { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

    }
}
