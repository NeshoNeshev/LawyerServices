﻿using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.AministrationViewModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IRequestsService requestService;
        private readonly ITownService townService;
        private readonly IImageService imageService;
        private readonly ILawyerService lawyerService;

        public AdministratorController(
            IImageService imageService, ITownService townService,
            IRequestsService requestService, IUserService userService,
            ILawyerService lawyerService, IWorkingTimeExceptionService wteService)
        {
            this.imageService = imageService;
            this.townService = townService;
            this.requestService = requestService;
            this.userService = userService;
            this.lawyerService = lawyerService;
        }

        [HttpPost("CreateUser")]
        public void CreateUser([FromBody] CreateLawyerModel lawyerModel)
        {
            //chseck
            if (!ModelState.IsValid)
            {

            }

            var response = this.lawyerService.CreateLawyer(lawyerModel);
            if (!response.IsCompleted)
            {

            }
            var companyId = response.Result;
            this.userService.CreateUserAsync(lawyerModel, companyId );
           
        }

        [HttpGet("GetAllRequests")]
        public async Task<IEnumerable<RequestViewModel>> GetAllRequests()
        {
            var allRequests =  this.requestService.GetAllRequests<RequestViewModel>();

            return allRequests;

        }

        [HttpGet("GetTowns")]
        public IEnumerable<TownViewModel> GetTowns()
        {
           //User.Identity.GetUserId();
            var towns = this.townService.GetAll<TownViewModel>();

            return towns;
        }
        [HttpGet("GetRequestsCount")]
        public int GetRequestsCount()
        { 
            var count = this.requestService.GetRequestsCount();

            return count;
        }
    }
}