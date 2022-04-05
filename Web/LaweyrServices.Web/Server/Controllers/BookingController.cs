﻿using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
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

        [HttpGet("GetLawyerWorkingTimeExteption")]
        public AppointmentViewModel GetLawyerWorkingTimeExteption(string appointmentId)
        {
            var appointment = this.lawyerService.GetLawyerWorkingTimeExteption(appointmentId);

            return appointment;
        }

        [HttpGet("GetLawyerById")]
        public IActionResult GetLawyerById(string lawyerId)
        {
            var exist = this.lawyerService.ExistingLawyerById(lawyerId);
            if (!exist)
            {
                return NotFound();
            }
            var lawyer = this.lawyerService.GetLawyerById(lawyerId);
            return Ok(lawyer);
        }

        [HttpPost("PostBooking")]
        public IActionResult PostBooking(UserRequestModel? userRequestModel)
        {

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null || !ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(userRequestModel),
                        "userId can not be null "
                        );
            }
            userRequestModel.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.wteService.SendRequestToLawyer(userRequestModel);
            if (!response.IsCompleted)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
    }
}