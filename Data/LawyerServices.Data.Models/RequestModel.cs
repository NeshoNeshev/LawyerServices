using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class RequestModel : BaseDeletableModel<string>
    {
        public string Names { get; set; }

        public string Languages { get; set; }
            
        public string Address { get; set; }

        public string Town { get; set; }

        public string Email { get; set; }

        public string Office { get; set; }
            
        public string WebSite { get; set; }

        public int YearsOfExperience { get; set; }


        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
