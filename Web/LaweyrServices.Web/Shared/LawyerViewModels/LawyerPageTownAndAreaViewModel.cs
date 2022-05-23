using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;

namespace LaweyrServices.Web.Shared.LawyerViewModels
{
    public class LawyerPageTownAndAreaViewModel
    {
        public IEnumerable<TownViewModel> Towns { get; set; }

        public IEnumerable<AreasOfActivityViewModel> Areas { get; set; }
    }
}
