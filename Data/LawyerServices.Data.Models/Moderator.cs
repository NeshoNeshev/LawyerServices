using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Moderator : BaseDeletableModel<string>
    {
        public Moderator()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        public string Email { get; set; }

        public string LawFirmId { get; set; }

        public LawFirm LawFirm { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
