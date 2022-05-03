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

        public SchedulerController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [Authorize(Roles = "Lawyer")]
        [HttpGet("GetAllAppointments")]
        public IList<Appointment> GetAllAppointments()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.companyService.GetAllAppointments(userId);

            return response;
        }

        [Authorize(Roles = "Lawyer")]
        [HttpGet("GetAllAppointmentsByCurrentDate")]
        public List<AppointmentViewModel> GetAllAppointmentsByCurrentDate(string date)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = this.companyService.GetCompanyId(userId);
            var response = this.companyService.GetAllAppointmentsByCurrentDate(date, lawyerId).ToList();

            return response;
        }

        [Authorize(Roles = "Lawyer")]
        [HttpPost("SaveCompanyAppointments")]
        public async Task<IActionResult> SaveCompanyAppointments([FromBody] Appointment data)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = companyService.GetCompanyId(userId);
            await this.companyService.SaveCompanyAppointmentsAsync(data, lawyerId);
            return Ok();


        }
        [Authorize(Roles = "Lawyer")]
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
