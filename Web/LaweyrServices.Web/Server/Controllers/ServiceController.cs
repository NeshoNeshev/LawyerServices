using LaweyrServices.Web.Shared.FixedCostModels;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly IFixedPriceService fixedPriceService;

        public ServiceController(ICompanyService companyService, IFixedPriceService fixedPriceService)
        {
            this.companyService = companyService;
            this.fixedPriceService = fixedPriceService;
        }


        [Authorize(Roles = "Lawyer")]
        [HttpGet("ServiceAndFeatures")]
        public FixedCostAndFeaturesViewModel GetFixedCostService()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = this.companyService.GetCompanyId(userId);

            var service = this.fixedPriceService.GetAll<FixedCostViewModel>(lawyerId);
            var features = this.companyService.GetFeatures(lawyerId);

            var model = new FixedCostAndFeaturesViewModel();
            model.fixedCostViewModel = service;
            model.featuresInputModel = features;

            //todo return
            return model;

        }

        [Authorize(Roles = "Lawyer")]
        [HttpPut("UpdateFeatures")]
        public async Task<IActionResult> UpdateFeatures([FromBody] FeaturesInputModel? model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var lawyerId = this.companyService.GetCompanyId(userId);

            if (lawyerId == null || model == null)
            {
                return BadRequest();
            }
            else
            {
                await this.companyService.UpdateFeaturesAsync(model, lawyerId);
            }

            return Ok(model); ;

        }

        [Authorize(Roles = "Lawyer")]
        [HttpPost("PostFixedCost")]
        public async Task<IActionResult> PostFixedCost([FromBody] FixedCostInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = this.companyService.GetCompanyId(userId);
            model.lawyerId = lawyerId;
            if (!this.ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(model),
                        "Areas can not be null "
                        );
            }
            var response = await this.fixedPriceService.CreateServiceAsyns(model);

            return Ok(response);


        }

        [Authorize(Roles = "Lawyer")]
        [HttpDelete("DeleteFixedCost")]
        public async Task<IActionResult> DeleteFixedCost(string serviceId)
        {
            if (String.IsNullOrEmpty(serviceId))
            {
                return BadRequest();
            }
            await this.fixedPriceService.DeleteServiceAsync(serviceId);

            return Ok();
        }

        [Authorize(Roles = "Lawyer")]
        [HttpPut("UpdateFixedCostService")]
        public async Task<IActionResult> UpdateFixedCostService([FromBody] FixedCostUpdateModel model)
        {
            if (!this.ModelState.IsValid || model == null)
            {
                return BadRequest();
            }
            else
            {
               await this.fixedPriceService.UpdateFixedCostServiceAsync(model);
            }

            return Ok(model); ;

        }
    }
}
