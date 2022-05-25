using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.NotaryModels;

namespace LaweyrServices.Web.Shared.IndexViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<TownViewModel> Towns { get; set; }

        public IEnumerable<AreasOfActivityViewModel> Areas { get; set; }

        public IEnumerable<LawFirmIndexViewModel> LawFirms { get; set; }

        public IEnumerable<NotaryIndexViewModel> AllNotarys { get; set; }
    }
}
