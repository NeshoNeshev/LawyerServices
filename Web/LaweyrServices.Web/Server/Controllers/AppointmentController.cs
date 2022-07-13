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
        private readonly IHtmlParser htmlParser;

        public AppointmentController(IWorkingTimeExceptionService wteService, ICompanyService companyService, IHtmlParser htmlParser)
        {
            this.wteService = wteService;
            this.companyService = companyService;
            this.htmlParser = htmlParser;
        }

        [HttpGet("GetAllRequsts")]
        public async Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllRequsts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = await this.wteService.GetAllRequsts(userId);

            return response;
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet("GetAllRequstsByLawyerId")]
        public async Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllRequstsByLawyerId(string lawyerId)
        {
          
            var response = await this.wteService.GetAllRequstsByLawyerId(lawyerId);

            return response;
        }
        [Authorize(Roles = "Lawyer,Moderator")]
        [HttpGet("GetAllMeetingByLawyerId")]
        public async Task<IEnumerable<WorkingTimeExceptionMeetingViewModel>> GetAllMeetingByLawyerId(string lawyerId)
        {
           
            var response = await this.wteService.GetMeetingWorkingTimeException(lawyerId);

            return response;
        }
        [Authorize(Roles = "Lawyer,Moderator")]
        [HttpGet("GetCourt")]
        public async Task<List<string>> GetCourt(string caseNumber, string caseYear, string courtId)
        {
            var response = await this.htmlParser.AngleSharpParseAsync(DateTime.Now.ToString("dd.MM.yyyy"), DateTime.Now.AddDays(30).ToString("dd.MM.yyyy"), caseNumber, caseYear, courtId);
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
        [Authorize(Roles = "Notary,Lawyer,Moderator")]
        [HttpPost("DeleteNotaryAppointment")]
        public async Task DeleteNotaryAppointment([FromBody]Appointment model)
        {
            
             await this.wteService.DeleteNotaryAppointmentAsync(model);
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
        public async Task<IActionResult> PostCanceledWte([FromBody] CancelCurrentWteInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            await this.wteService.SetIsCanceledAsync(model, lawyerId);
            return Ok();

        }
        [Authorize(Roles = "Moderator")]
        [HttpPut("PostCanceledWteByModerator")]
        public async Task<IActionResult> PostCanceledWteByModerator([FromBody] CancelCurrentWteInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           
            await this.wteService.SetIsCanceledAsync(model, model.LawyerId);
            return Ok();

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
