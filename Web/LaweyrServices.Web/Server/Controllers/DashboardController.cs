using LaweyrServices.Web.Shared.DashboardModels;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IWorkingTimeExceptionService wteService;
        private readonly ICompanyService companyService;
        private readonly ILawFirmService lawFirmService;
        private readonly ITimeService timeService;
        public DashboardController(IWorkingTimeExceptionService wteService, ICompanyService companyService, ILawFirmService lawFirmService, ITimeService timeService)
        {
            this.wteService = wteService;
            this.companyService = companyService;
            this.lawFirmService = lawFirmService;
            this.timeService = timeService;
        }



        [Authorize(Roles = "Lawyer")]
        [HttpGet("GetAllRequestsByDayOfWeek")]
        public async Task<LawyerDasboardViewModel> GetAllRequestsByDayOfWeek()
        {
            var date = this.timeService.GetTimeOffset(DateTime.Now);
            var response = new LawyerDasboardViewModel();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            var clintCount = await this.wteService.GetUserRequstsCountAsync(lawyerId);
            var meetingCount = await this.wteService.GetMeetingRequstsCountAsync(lawyerId);
            var wtExceptions = await this.wteService.GetAllRequestsByDayOfWeekAsync(lawyerId, date);
            var usersCount = await this.companyService.GetUsersInCompanyCountAsync(lawyerId);
            var lawFirmId = await this.lawFirmService.GetLawFirmIdAsync(lawyerId);
            var meetings = await this.wteService.GetAllRequestsByDayOfWeekMeetingAsync(lawyerId, date);
            response.MeetingtCount = meetingCount;
            response.ClientCount = clintCount;
            response.wteModel = wtExceptions;
            response.UsersCount = usersCount;
            response.LawFirmId = lawFirmId;
            response.meetingModel = meetings;
            return response;
        }
    }
}
