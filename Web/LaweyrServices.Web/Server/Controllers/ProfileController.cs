
using LaweyrServices.Web.Shared.ProfileModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ICurrentProfileService profileService;
        private readonly ICompanyService companyService;
        private readonly IUserService userService;
        public ProfileController(ICurrentProfileService profileService, ICompanyService companyService, IUserService userService)
        {
            this.profileService = profileService;
            this.companyService = companyService;
            this.userService = userService;
        }

        [HttpGet("GetLawyerProfileInformation")]
        public LawyerProfileViewModel GetLawyerProfileInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = this.userService.GetUserInformation(userId);
            
            var lawyerId = this.companyService.GetCompanyId(userId);
            var response = this.profileService.GetLawyerProfileInformation(lawyerId);
            response.PhoneNumber = user.PhoneNumber;

            return response;
        }

        [HttpPut("EditLawyerProfileInformation")]
        public async Task<IActionResult> EditLawyerProfileInformation([FromBody] EditLawyerProfileModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = this.companyService.GetCompanyId(userId);
            model.Id = lawyerId;
            model.userId = userId;
            await this.profileService.EditLawyerProfileInformationAsync(model);


            return Ok();
        }
    }
}
