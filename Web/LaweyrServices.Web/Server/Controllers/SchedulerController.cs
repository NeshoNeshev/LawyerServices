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
        public IActionResult SaveCompanyAppointments([FromBody] Appointment data)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.companyService.SaveCompanyAppointments(data, userId);
            if (response != null) return Ok();

            return BadRequest();
        }
        [Authorize(Roles = "Lawyer")]
        [HttpPut("EditCompanyAppointments")]
        public IActionResult EditCompanyAppointments([FromBody] Appointment data)
        {
           
            var response = this.companyService.EditAppointment(data);
            if (response != null) return Ok();

            return BadRequest();
        }
    }
}
