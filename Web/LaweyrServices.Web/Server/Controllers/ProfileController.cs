
using LaweyrServices.Web.Shared.ProfileModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Lawyer")]
    public class ProfileController : ControllerBase
    {
        private readonly ICurrentProfileService profileService;
        private readonly ICompanyService companyService;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        public ProfileController(ICurrentProfileService profileService, ICompanyService companyService, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            this.profileService = profileService;
            this.companyService = companyService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpGet("GetLawyerProfileInformation")]
        public async Task<LawyerProfileViewModel> GetLawyerProfileInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await this.userService.GetUserInformationAsync(userId);
            
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            var response = await this.profileService.GetLawyerProfileInformationAsync(lawyerId);
            response.PhoneNumber = user.PhoneNumber;
            response.Email = user.Email;
            return response;
        }

        [HttpPut("EditLawyerProfileInformation")]
        public async Task<IActionResult> EditLawyerProfileInformation([FromBody] EditLawyerProfileModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            //var exist = await this.userManager.FindByEmailAsync(model.Email);
            //if (exist.Email == null)
            //{
            //    return BadRequest();
            //}
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            model.Id = lawyerId;
            model.userId = userId;
            await this.profileService.EditLawyerProfileInformationAsync(model);


            return Ok();
        }
        
    }
}
