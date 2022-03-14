using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.UserModels;
using System.Security.Claims;

namespace LawyerServices.Services.Data.AdminServices
{
    public interface IUserService
    {
        public IEnumerable<T> GetAll<T>(int? count = null);
        public string? GetUserId(ClaimsPrincipal principal);

        public void CreateUserAsync(CreateLawyerModel lawyerModel, string companyId);

        public ApplicationUserViewModel GetUserInformation(string userId);
    }
}
