using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Court : BaseDeletableModel<string>
    {
        public string? Name { get; set; }

        public string? CourtUrl { get; set; }

        public string? TownId { get; set; }

        public virtual Town? Town { get; set; }
    }
}
