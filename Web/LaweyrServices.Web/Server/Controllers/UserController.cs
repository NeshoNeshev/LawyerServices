using LaweyrServices.Web.Shared.LawyerViewModels;
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

        public UserController(ICompanyService companyService, ILawyerService lawyerService)
        {
            this.companyService = companyService;
            this.lawyerService = lawyerService;
        }

        [HttpGet("GetInformation")]
        public LawyerListItem GetInformation()
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
    }
}
