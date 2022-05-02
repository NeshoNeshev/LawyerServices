using LaweyrServices.Web.Shared.AministrationViewModels;

namespace LaweyrServices.Web.Shared.NotaryModels
{
    public class NotaryPageViewModel
    {
        public IEnumerable<NotaryViewModel> AllNotarys { get; set; }

        public IEnumerable<TownViewModel> AllTowns { get; set; }
    }
}
