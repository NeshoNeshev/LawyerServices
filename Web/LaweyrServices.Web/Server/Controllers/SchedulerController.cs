using LaweyrServices.Web.Shared.DateModels;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SchedulerController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly ITimeService timeService;

        public SchedulerController(ICompanyService companyService, ITimeService timeService)
        {
            this.companyService = companyService;
            this.timeService = timeService;
        }

        [Authorize(Roles = "Lawyer, Notary")]
        [HttpGet("GetAllAppointments")]
        public async Task<IList<Appointment>> GetAllAppointments()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = await this.companyService.GetAllAppointmentsAsync(userId);

            return response;
        }

        [Authorize(Roles = "Lawyer, Notary")]
        [HttpGet("GetAllAppointmentsByCurrentDate")]
        public async Task<List<AppointmentViewModel>> GetAllAppointmentsByCurrentDate()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            var date = this.timeService.GetTimeOffset(DateTime.Now);
            var response = this.companyService.GetAllAppointmentsByCurrentDate(date, lawyerId).ToList();

            return response;
        }

        [Authorize(Roles = "Lawyer, Notary")]
        [HttpPost("SaveCompanyAppointments")]
        public async Task<IActionResult> SaveCompanyAppointments([FromBody] Appointment data)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await companyService.GetCompanyIdAsync(userId);
            await this.companyService.SaveCompanyAppointmentsAsync(data, lawyerId);
            return Ok();


        }
        [Authorize(Roles = "Lawyer, Notary")]
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
    }
}
