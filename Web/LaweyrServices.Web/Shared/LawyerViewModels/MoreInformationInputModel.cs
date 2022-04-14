using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LaweyrServices.Web.Shared.LawyerViewModels
{
    public class MoreInformationInputModel : IMapFrom<Company>
    {
        public string? Languages { get; set; }

        public string? AboutText { get; set; }

        public string? HeaderText { get; set; }

        public int? YearFirstAdmitted { get; set; }

        public string? WebSite { get; set; }

        public string? ImgUrl { get; set; }
    }
}
