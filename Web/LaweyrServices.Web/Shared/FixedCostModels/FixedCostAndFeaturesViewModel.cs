using LaweyrServices.Web.Shared.AreasOfActivityViewModels;

namespace LaweyrServices.Web.Shared.FixedCostModels
{
    public class FixedCostAndFeaturesViewModel
    {
        public IEnumerable<FixedCostViewModel>? fixedCostViewModel { get; set; }

        public FeaturesInputModel? featuresInputModel { get; set; }

        public IEnumerable<AreasOfActivityViewModel>? Areas { get; set; }

        public IEnumerable<AreasOfActivityViewModel>? LawyerAreas { get; set; }
    }
}
