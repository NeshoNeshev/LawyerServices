using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Address : BaseDeletableModel<string>
    {
        public string AddressLocation { get; set; }

        public string TownId { get; set; }

        public Town Town { get; set; }
    }
}
