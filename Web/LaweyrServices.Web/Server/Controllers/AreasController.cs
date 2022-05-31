using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly IAreasOfActivityService areaService;

        private readonly ICompanyService companyService;

        public AreasController(IAreasOfActivityService areaService, ICompanyService companyService)
        {
            this.areaService = areaService;
            this.companyService = companyService;
        }

        [Authorize(Roles = "Lawyer")]
        [HttpGet("GetAllAreas")]
        public async Task<IEnumerable<AreasOfActivityViewModel>> GetAllAreas()
        {
            var areas = await this.areaService.GetAll<AreasOfActivityViewModel>();

            return areas;
        }

        [Authorize(Roles = "Lawyer")]
        [HttpGet("GetAllAreasByCompanyId")]
        public async Task<IEnumerable<AreasOfActivityViewModel>> GetAllAreasByCompanyId()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            var areas = await areaService.GetAllAreasByCompanyId(lawyerId);

            return areas;
        }

        [Authorize(Roles = "Lawyer")]
        [HttpPost("OnSaveInformation")]
        public async Task<IActionResult> OnSaveInformation([FromBody] MoreInformationInputModel? moreInformation)
        {
            if (moreInformation == null || !this.ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(moreInformation),
                        "More information can not be null "
                        );
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = await this.companyService.CreateMoreInformationAsync(moreInformation, userId);
            return Ok(response);
        }

        [Authorize(Roles = "Lawyer")]
        [HttpPost("CreateAreas")]
        public async Task<IActionResult> CreateAreas([FromBody] string areaName)
        {
            if (areaName is null)
            {
                
                return BadRequest();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
          
            await this.areaService.CreateAreasAsync(areaName, lawyerId);
            return Ok();
        }
        [Authorize(Roles = "Lawyer")]
        [HttpDelete("DeleteArea")]
        public async Task<IActionResult> DeleteArea(string areaId)
        {
            if (String.IsNullOrEmpty(areaId))
            {
                return BadRequest();
            }
         
            await this.areaService.DeleteAreaAsync(areaId);

            return Ok();
        }
    }
}
