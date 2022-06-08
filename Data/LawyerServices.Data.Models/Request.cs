using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class Request : BaseDeletableModel<string>
    {
        public Request()
        {
            this.Companies = new HashSet<Company>();
        }
        public string Names { get; set; }

        public string Address { get; set; }

        public string Town { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool? IsApproved { get; set; } = false;

        public virtual ICollection<Company> Companies { get; set; }
    }
}
