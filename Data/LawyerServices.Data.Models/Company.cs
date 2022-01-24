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
        }

        public string Names { get; set; }

        public string? WebSite { get; set; }

        public string Address { get; set; }

        public string? OfficeName { get; set; }

        public string? Languages { get; set; }

        public string? Education { get; set; }

        public string? Qualifications { get; set; }

        public string? Experience { get; set; }

        public string? ImgUrl { get; set; }

        public Profession Profession { get; set; }

        public string TownId { get; set; }

        public virtual Town Town { get; set; }

        public string WorkingTimeId { get; set; }

        public WorkingTime WorkingTime { get; set; }

        public virtual ICollection<AreasCompany> AreasCompanies { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
