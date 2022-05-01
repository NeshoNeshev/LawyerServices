using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly ILawyerService lawyerService;
        private readonly IUserService userService;
        private readonly IWorkingTimeExceptionService workingTimeExceptionService;

        public UserController(ICompanyService companyService, ILawyerService lawyerService, IUserService userService, IWorkingTimeExceptionService workingTimeExceptionService)
        {
            this.companyService = companyService;
            this.lawyerService = lawyerService;
            this.userService = userService;
            this.workingTimeExceptionService = workingTimeExceptionService;
        }

        [HttpGet("GetInformation")]
        public LawyerListItem GetLawyerInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var companyId = this.companyService.GetCompanyId(userId);
            var response = this.lawyerService.GetLawyerById(companyId);

            return response;
           
        }

        [HttpGet("UserInformation")]
        public ApplicationUserViewModel GetUserInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = this.userService.GetUserInformation(userId);

            return user;
        }

        [HttpGet("GetUserWorkingTimeExceptions")]
        public List<WorkingTimeExceptionUserViewModel> GetUserWorkingTimeExceptions()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.workingTimeExceptionService.GetRequestsForUserId(userId).ToList();

            return response;
        
        }

        [HttpPut("SetWorkingTimeExceptionToFree")]
        public async Task<IActionResult> SetWorkingTimeExceptionToFree([FromBody] string wteId)
        {
            if (wteId == null)
            {
                return BadRequest();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.workingTimeExceptionService.SetWorkingTimeExceptionToFreeAsync(wteId, userId);

            return Ok();
        }
    }
}
