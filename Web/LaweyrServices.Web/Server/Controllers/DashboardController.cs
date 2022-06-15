﻿using LaweyrServices.Web.Shared.DashboardModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
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
        private readonly ILawyerService lawyerService;
        private readonly IDateTmeManipulatorService dateTmeManipulator;
        public DashboardController(IWorkingTimeExceptionService wteService, ICompanyService companyService, ILawFirmService lawFirmService, ILawyerService lawyerService, IDateTmeManipulatorService dateTmeManipulator)
        {
            this.wteService = wteService;
            this.companyService = companyService;
            this.lawFirmService = lawFirmService;
            this.lawyerService = lawyerService;
            this.dateTmeManipulator = dateTmeManipulator;
        }



        [Authorize(Roles = "Lawyer")]
        [HttpGet("GetAllRequestsByDayOfWeek")]
        public async Task<LawyerDasboardViewModel> GetAllRequestsByDayOfWeek(string date)
        {
            var currentDate = this.dateTmeManipulator.ConvertStringToDateTime(date);
            var response = new LawyerDasboardViewModel();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);
            var clintCount = await this.wteService.GetUserRequstsCountAsync(lawyerId);
            var meetingCount = await this.wteService.GetMeetingRequstsCountAsync(lawyerId);
            var wtExceptions = await this.wteService.GetAllRequestsByDayOfWeekAsync(lawyerId, currentDate);
            var usersCount = await this.companyService.GetUsersInCompanyCountAsync(lawyerId);
            var lawFirmId = await this.lawFirmService.GetLawFirmIdAsync(lawyerId);
            var meetings = await this.wteService.GetAllRequestsByDayOfWeekMeetingAsync(lawyerId, currentDate);
            var isOwner = await this.lawyerService.IsOwner(lawyerId);
            response.MeetingtCount = meetingCount;
            response.ClientCount = clintCount;
            response.wteModel = wtExceptions;
            response.UsersCount = usersCount;
            response.LawFirmId = lawFirmId;
            response.IsOwner = isOwner;
            response.meetingModel = meetings;
            return response;
        }
    }
}
