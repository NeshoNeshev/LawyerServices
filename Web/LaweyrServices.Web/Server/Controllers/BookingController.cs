using LaweyrServices.Web.Shared.BookingModels;
using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Treblle.Net.Core;

namespace LaweyrServices.Web.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILawyerService lawyerService;
        private readonly IWorkingTimeExceptionService wteService;
        private readonly IUserService userService;
        private readonly ITimeService timeService;
        public BookingController(ILawyerService lawyerService, IWorkingTimeExceptionService wteService, IUserService userService, ITimeService timeService)
        {
            this.lawyerService = lawyerService;
            this.wteService = wteService;
            this.userService = userService;
            this.timeService = timeService;
        }
        [AllowAnonymous]
        [HttpGet("GetLawyerWorkingTimeExteption")]
        public async Task<AppointmentViewModel> GetLawyerWorkingTimeExteption([FromQuery] string appointmentId)
        {
            var appointment = await this.lawyerService.GetLawyerWorkingTimeExteption(appointmentId);

            return appointment;
        }
  
        [HttpGet("GetBookingInformation")]
        public async Task<IActionResult> GetBookingInformation(string lawyerId, string appointmentId)
        {
            if (lawyerId == null || appointmentId == null)
            {
                return BadRequest();
            }

            var result = new BookingViewModel();

            if (this.User.Identity.IsAuthenticated)
            {
                var date = this.timeService.GetTimeOffset(DateTime.Now);
                var userId = this.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
                result.ApplicationUserViewModel = await this.userService.GetUserInformationAsync(userId);
                var countUp = await this.userService.UserNextWteNumberIsSmalForTwo(userId, date);
                result.ApplicationUserViewModel.CountUp = countUp;
            }
            var lawyer = await this.lawyerService.GetLawyerAsync<LawyerBookingViewModel>(lawyerId);
            if (lawyer != null) result.LawyerBookingViewModel = lawyer;

            result.AppointmentViewModel = await this.lawyerService.GetLawyerWorkingTimeExteption(appointmentId);
            //result.ApplicationUserViewModel = await this.userService.GetUserInformationAsync(userId);

            return Ok(result);
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
        [Treblle]
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
            var date = this.timeService.GetTimeOffset(DateTime.Now);
            if (lawyerId == null)
            {
                return BadRequest();
            }
            var wte = await this.wteService.GetEarliestWteAsync(lawyerId.ToLower() , date);

            return Ok(wte);
        }
    }
}
