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
        public IActionResult PostApproved([FromBody] string Id)
        {
            if (Id != null)
            {
                var response = this.wteService.SetIsApproved(Id);
                return Ok(response);
            }

            return BadRequest();

        }

        [Authorize(Roles = "Lawyer")]
        [HttpPut("PostNotShowUp")]
        public IActionResult PostNotShowUp([FromBody] string Id)
        {
            if (Id != null)
            {
                var response = this.wteService.SetNotSHowUp(Id);
                if (response.Result == false)
                {
                    return BadRequest("id canot by null");
                }
                return Ok(response);
            }

            return BadRequest();

        }
    }
}
