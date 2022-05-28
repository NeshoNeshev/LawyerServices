using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IWorkingTimeExceptionService wteService;
        private readonly ICompanyService companyService;
        public AppointmentController(IWorkingTimeExceptionService wteService, ICompanyService companyService)
        {
            this.wteService = wteService;
            this.companyService = companyService;
        }

        [HttpGet("GetAllRequsts")]
        public List<WorkingTimeExceptionBookingModel> GetAllRequsts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.wteService.GetAllRequsts(userId).ToList();

            return response;
        }
        [Authorize(Roles = "Notary")]
        [HttpGet("GetAllNotaryRequsts")]
        public async Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllNotaryRequsts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = await this.wteService.GetAllNotaryRequsts(userId);

            return response;
        }
        [Authorize(Roles = "Lawyer")]
        [HttpGet("GetAllMeeting")]
        public async Task<IEnumerable<WorkingTimeExceptionMeetingViewModel>>  GetAllMeeting()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
           var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            var response = await this.wteService.GetMeetingWorkingTimeException(lawyerId);

            return response;
        }

        [Authorize(Roles = "Lawyer")]
        [HttpPut("PostCanceledWte")]
        public async Task<IActionResult> PostCanceledWte([FromBody] string Id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            if (Id != null)
            {
                await this.wteService.SetIsCanceledAsync(Id, lawyerId);
                return Ok();
            }

            return BadRequest();

        }

        [Authorize(Roles = "Lawyer")]
        [HttpPut("PostNotShowUp")]
        public async Task<IActionResult> PostNotShowUp([FromBody] string Id)
        {
            if (Id != null)
            {
                var response = await this.wteService.SetNotSHowUpAsync(Id);
                if (response == false)
                {
                    return BadRequest("id canot by null");
                }
                return Ok(response);
            }

            return BadRequest();

        }
        [Authorize(Roles = "Lawyer")]
        [HttpPut("CancelAppointmentInRange")]
        public async Task<IActionResult> CancelAppointmentInRange([FromBody] CancelAppointmentInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            if (this.ModelState.IsValid)
            {
                await this.wteService.CancelAppointmentInRangeAsync(model, lawyerId);
                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Lawyer")]
        [HttpPut("CancelAppointmentFromDate")]
        public async Task<IActionResult> CancelAppointmentFromDate([FromBody] CancelAppointmentForOneDateInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            await this.wteService.CancelAppointmentFromDateAsync(model, lawyerId);
            return Ok();
        }
    }
}
