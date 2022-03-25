using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Review : BaseDeletableModel<string>
    {
        public byte Rating { get; set; }

        public string? Commentary { get; set; }

        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public string? CompanyId { get; set; }

        public virtual Company? Company { get; set; }
    }
}
