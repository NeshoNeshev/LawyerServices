using LaweyrServices.Web.Shared.AministrationViewModels;

namespace LaweyrServices.Web.Shared.NotaryModels
{
    public class NotaryPageViewModel
    {
        public ICollection<NotaryViewModel> AllNotarys { get; set; }

        public IEnumerable<TownViewModel> AllTowns { get; set; }
    }
}
