using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILawyerService lawyerService;
        private readonly IWorkingTimeExceptionService wteService;


        public BookingController(ILawyerService lawyerService, IWorkingTimeExceptionService wteService)
        {
            this.lawyerService = lawyerService;
            this.wteService = wteService;
        }
        [AllowAnonymous]
        [HttpGet("GetLawyerWorkingTimeExteption")]
        public async Task<AppointmentViewModel> GetLawyerWorkingTimeExteption([FromQuery]string appointmentId)
        {
            var appointment = await this.lawyerService.GetLawyerWorkingTimeExteption(appointmentId);

            return appointment;
        }

        [HttpGet("GetLawyerById")]
        public async Task<IActionResult> GetLawyerById(string lawyerId)
        {
            var exist = this.lawyerService.ExistingLawyerById(lawyerId);
            if (!exist)
            {
                return NotFound();
            }
            var lawyer = await this.lawyerService.GetLawyerByIdAsync(lawyerId);
            return Ok(lawyer);
        }

        [Authorize(Roles = "User")]
        [HttpPost("PostBooking")]
        public async Task<IActionResult> PostBooking(UserRequestModel? userRequestModel)
        {

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null || !ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(userRequestModel),
                        "userId can not be null "
                        );
                return BadRequest();
            }
            userRequestModel.UserId = userId;
            await this.wteService.SendRequestToLawyerAsync(userRequestModel);
           
            return Ok();

        }
         
        [Authorize(Roles = "User")]
        [HttpGet("FreeWte")]
        public async Task<IActionResult> FreeWte(string? wteId)
        {
            if (wteId == null)
            {
                return BadRequest();
            }
            var exist = await this.wteService.FreeRequestByWteIdAsync(wteId);
            return Ok(exist);
        }

        [Authorize(Roles = "User")]
        [HttpGet("EarlyTime")]
        public async Task<IActionResult> EarlyTime(string? lawyerId)
        {
            var model = new EarlyTimeModel();
            if (lawyerId == null)
            {
                return BadRequest();
            }
            var wte = await this.wteService.GetEarliestWteAsync(lawyerId);
          
            
            return Ok(wte);
        }
    }
}
