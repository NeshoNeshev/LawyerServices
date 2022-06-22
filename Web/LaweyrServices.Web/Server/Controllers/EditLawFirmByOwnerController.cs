using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.FixedCostModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Moderator")]
    public class EditLawFirmByOwnerController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly ILawFirmService lawFirmService;
        private readonly IFixedPriceService fixedPriceService;
        private readonly IWorkingTimeExceptionService workingTimeExceptionService;
        private readonly IModeratorService moderatorService;

        public EditLawFirmByOwnerController(ICompanyService companyService, IWorkingTimeExceptionService workingTimeExceptionService, ILawFirmService lawFirmService, IFixedPriceService fixedPriceService, IModeratorService moderatorService)
        {
            this.companyService = companyService;
            this.workingTimeExceptionService = workingTimeExceptionService;
            this.lawFirmService = lawFirmService;
            this.fixedPriceService = fixedPriceService;
            this.moderatorService = moderatorService;
        }


        [HttpGet("ServiceAndFeatures")]
        public FixedCostAndFeaturesViewModel GetFixedCostService(string lawyerId)
        {

            var service = this.fixedPriceService.GetAll<FixedCostViewModel>(lawyerId);
            var features = this.companyService.GetFeatures(lawyerId);

            var model = new FixedCostAndFeaturesViewModel();
            model.fixedCostViewModel = service;
            model.featuresInputModel = features;

            return model;

        }
   
        [HttpPost("PostFixedCost")]
        public async Task<IActionResult> PostFixedCost([FromBody] FixedCostInputModel model)
        {
           
            if (!this.ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(model),
                        "Areas can not be null "
                        );
            }
            var response = await this.fixedPriceService.CreateServiceAsyns(model);

            return Ok(response);


        }
     
        [HttpPut("UpdateFeatures")]
        public async Task<IActionResult> UpdateFeatures([FromBody] FeaturesInputModel? model)
        {
           

            if (model == null)
            {
                return BadRequest();
            }
            else
            {
                await this.companyService.UpdateFeaturesAsync(model, model.LawyerId);
            }

            return Ok(model); ;

        }
       
        [HttpGet("GetAllAppointments")]
        public IList<Appointment> GetAllAppointments(string lawyerId)
        {
            
            var response = this.companyService.GetAllAppointmentsByLawyerId(lawyerId);

            return response;
        }

   
        [HttpGet("GetLawFirmInformation")]
        public async Task<IActionResult> GetLawFirmInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var moderatorId = await this.moderatorService.GetModeratorId(userId);
            var lawFirm = await this.lawFirmService.GetLawFirmByLawyerId(moderatorId);
            
            if (lawFirm == null)
            {
                return BadRequest();
            }

            return Ok(lawFirm);
        }

        [HttpPost("SaveCompanyAppointments")]
        public async Task<IActionResult> SaveCompanyAppointments([FromBody] Appointment data, [FromQuery] string lawyerId)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            await this.companyService.SaveCompanyAppointmentsAsync(data, lawyerId);
            return Ok();


        }

        [HttpPut("EditCompanyAppointments")]
        public async Task<IActionResult> EditCompanyAppointments([FromBody] Appointment data)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            await this.companyService.EditAppointmentAsync(data);

            return Ok();
        }

        [HttpPut("CancelAppointmentInRange")]
        public async Task<IActionResult> CancelAppointmentInRange([FromBody] CancelAppointmentInputModel model)
        {

            if (this.ModelState.IsValid)
            {
                await this.workingTimeExceptionService.CancelAppointmentInRangeAsync(model, model.LawyerId);
                return Ok();
            }

            return BadRequest();
        }


        [HttpPut("CancelAppointmentFromDate")]
        public async Task<IActionResult> CancelAppointmentFromDate([FromBody] CancelAppointmentForOneDateInputModel model)
        {
            
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            await this.workingTimeExceptionService.CancelAppointmentFromDateAsync(model, model.LawyerId);
            return Ok();
        }
        [HttpPut("EditLawFirm")]
        public async Task<IActionResult> EditLawFirm([FromBody] EditLawFirmAdministrationModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.lawFirmService.EditLawFirmAsync(model);
                return Ok();
            }
            return BadRequest();

        }
    }
}
