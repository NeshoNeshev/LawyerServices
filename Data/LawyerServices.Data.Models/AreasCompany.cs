using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class AreasCompany : BaseDeletableModel<string>
    {
        public string CompanyId { get; set; }

        public Company Company { get; set; }

        public string AreasOfActivityId { get; set; }

        public AreasOfActivity AreasOfActivity { get; set; }
    }
}
