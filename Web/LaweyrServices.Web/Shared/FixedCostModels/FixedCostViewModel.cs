using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.FixedCostModels
{
    public class FixedCostViewModel : IMapFrom<FixedCostService>
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public double? Price { get; set; }
    }
}
