using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class FixedCostService : BaseDeletableModel<string>
    {
        public FixedCostService()
        {

        }

        public string? Name { get; set; }

        public double? Price { get; set; }

        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
