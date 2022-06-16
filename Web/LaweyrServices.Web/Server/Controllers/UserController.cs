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
        private readonly ITimeService timeService;
        public UserController(ICompanyService companyService, ILawyerService lawyerService, IUserService userService, IWorkingTimeExceptionService workingTimeExceptionService, ITimeService timeService)
        {
            this.companyService = companyService;
            this.lawyerService = lawyerService;
            this.userService = userService;
            this.workingTimeExceptionService = workingTimeExceptionService;
            this.timeService = timeService;
        }

        [HttpGet("GetInformation")]
        public async Task<LawyerListItem> GetLawyerInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var companyId = await this.companyService.GetCompanyIdAsync(userId);
            var response = await this.lawyerService.GetLawyerByIdAsync(companyId);

            return response;
           
        }

        [HttpGet("UserInformation")]
        public async Task<ApplicationUserViewModel> GetUserInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await this.userService.GetUserInformationAsync(userId);

            return user;
        }

        [HttpGet("GetNextUserWorkingTimeExceptions")]
        public async Task<IEnumerable<WorkingTimeExceptionUserViewModel>> GetUserWorkingTimeExceptions()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
           
            var response = await this.workingTimeExceptionService.GetRequestsForUserIdAsync(userId);

            return response;
        
        }
        [HttpGet("GetOverUserWorkingTimeExceptions")]
        public async Task<IEnumerable<WorkingTimeExceptionUserViewModel>> GetOverUserWorkingTimeExceptions()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var date = this.timeService.GetTimeOffset(DateTime.Now);
            var response = await this.workingTimeExceptionService.GetOverRequestsForUserIdAsync(userId, date);

            return response;

        }
        [HttpPut("SetWorkingTimeExceptionToFree")]
        public async Task<IActionResult> SetWorkingTimeExceptionToFree([FromBody] WorkingTimeExceptionUserViewModel? Wte)
        {
            if (Wte == null)
            {
                return BadRequest();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.workingTimeExceptionService.SetWorkingTimeExceptionToFreeAsync(Wte.Id, userId, Wte.WorkingTime.Companies.First().Id);

            return Ok();
        }

        [HttpPut("EditUserByUser")]
        public async Task<IActionResult> EditUserByUser([FromBody] EditUserInformationInputModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.Id = userId;
            await this.userService.EditUserProfileByUserAsync(model);

            return Ok();
        }
    }
}
