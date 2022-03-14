using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly ILawyerService lawyerService;
        private readonly IUserService userService;

        public UserController(ICompanyService companyService, ILawyerService lawyerService, IUserService userService)
        {
            this.companyService = companyService;
            this.lawyerService = lawyerService;
            this.userService = userService;
        }

        [HttpGet("GetInformation")]
        public LawyerListItem GetLawyerInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var companyId = this.companyService.GetCompanyId(userId);
            var response = this.lawyerService.GetLawyerById(companyId);

            return response;
            //if (companyId != null)
            //{

            //}
            //else
            //{

            //}
        }

        [HttpGet("UserInformation")]
        public ApplicationUserViewModel GetUserInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = this.userService.GetUserInformation(userId);

            return user;
        }
    }
}
