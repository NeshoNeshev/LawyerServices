using LawyerServices.Data.Models;
using LawyerServices.Services.Mapping;

namespace LawyerServices.Web.Shared.AdministrationModels
{
    public class UsersViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
