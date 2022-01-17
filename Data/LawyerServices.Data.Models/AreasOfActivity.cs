using LawyerServices.Data.Models.SystemModels;

namespace LawyerServices.Data.Models
{
    public class AreasOfActivity : BaseDeletableModel<string>
    {
        public AreasOfActivity()
        {
            this.AreasCompanies = new HashSet<AreasCompany>();
        }

        public string Name { get; set; }

        public string? BindingName { get; set; }

        public virtual ICollection<AreasCompany> AreasCompanies { get; set; }
  
    }
}