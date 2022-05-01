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

        public AppointmentController(IWorkingTimeExceptionService wteService)
        {
            this.wteService = wteService;
        }

        [HttpGet("GetAllRequsts")]
        public List<WorkingTimeExceptionBookingModel> GetAllRequsts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.wteService.GetAllRequsts(userId).ToList();

            return response;
        }


        [Authorize(Roles = "Lawyer")]
        [HttpPut("PostApproved")]
        public async Task<IActionResult> PostApproved([FromBody] string Id)
        {
            if (Id != null)
            {
                await this.wteService.SetIsApprovedAsync(Id);
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
            if (this.ModelState.IsValid)
            {
                await this.wteService.CancelAppointmentInRangeAsync(model, userId);
                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Lawyer")]
        [HttpPut("CancelAppointmentFromDate")]
        public async Task<IActionResult> CancelAppointmentFromDate([FromBody] CancelAppointmentForOneDateInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
           
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            await this.wteService.CancelAppointmentFromDateAsync(model, userId);
            return Ok();
        }
    }
}
