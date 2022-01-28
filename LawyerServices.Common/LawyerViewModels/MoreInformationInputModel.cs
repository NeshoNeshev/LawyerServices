using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Common.LawyerViewModels
{
    public class MoreInformationInputModel : IMapFrom<Company>
    {
        public string Languages { get; set; }

        public string Education { get; set; }

        public string Qualifications { get; set; }

        public string Experience { get; set; }

        public string WebSite { get; set; }

        public string ImgUrl { get; set; }
    }
}
