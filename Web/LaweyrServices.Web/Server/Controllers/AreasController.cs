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
        public List<AreasOfActivityViewModel> GetAllAreas()
        {
            var areas = this.areaService.AllAreas().ToList();

            return areas;
        }

        [Authorize(Roles = "Lawyer")]
        [HttpGet("GetAllAreasByCompanyId")]
        public List<string> GetAllAreasByCompanyId()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var areas = areaService.GetAllAreasByCompanyId(userId).ToList();

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
        public async Task<IActionResult> CreateAreas([FromBody] IList<string>? areasToAdd)
        {
            if (areasToAdd == null || !this.ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(areasToAdd),
                        "Areas can not be null "
                        );
                return BadRequest();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.areaService.CreateAreasAsync(areasToAdd, userId);
            return Ok();
        }
    }
}
