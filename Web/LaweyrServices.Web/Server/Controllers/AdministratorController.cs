﻿using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly ILawyerService lawyerService;
        private readonly INotaryService notaryService;
        private readonly ILawFirmService lawyfirmService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdministratorController(
            IImageService imageService, ITownService townService,
            IRequestsService requestService,
            ILawyerService lawyerService, IWorkingTimeExceptionService wteService, INotaryService notaryService, IUserService userService, ILawFirmService lawyfirmService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.townService = townService;
            this.requestService = requestService;
            this.userService = userService;
            this.lawyerService = lawyerService;
            this.notaryService = notaryService;
            this.userService = userService;
            this.lawyfirmService = lawyfirmService;
            this.signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateLawyerModel lawyerModel)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(lawyerModel),
                        "Invalid model"
                        );

            }

            var response = await this.lawyerService.CreateLawyerAsync(lawyerModel);
            if (response == null)
            {
                return BadRequest();
            }
            var companyId = response;
            this.userService.CreateUserAsync(lawyerModel, companyId);

            return Ok();
            //todo
            //this.requestService.SetIsApproved();
        }

        [HttpPost("CreateNotary")]
        public async Task<IActionResult> CreateNotary([FromBody] CreateNotaryModel notaryModel)
        {
            var existigPhone = this.userService.ExistingPhoneNumber(notaryModel.PhoneNumber);
            //chseck
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(notaryModel),
                        "Invalid model"
                        );

            }
            if (existigPhone == true)
            {
                ModelState.AddModelError(nameof(notaryModel.PhoneNumber),
                       "Телефонът съществува"
                       );

                return BadRequest(ModelState);
            }
            var response = await this.notaryService.CreateNotaryAsync(notaryModel);
            if (response == null)
            {

            }
            var companyId = response;
            this.userService.CreateNotaryUserAsync(notaryModel, companyId);
            return Ok();
            //todo
            //this.requestService.SetIsApproved();
        }

        [HttpPost("CreateLawFirm")]
        public async Task<IActionResult> CreateLawFirm([FromBody] LawFirmInputModel lawFirmModel)
        {
            //chseck
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(lawFirmModel),
                        "Invalid model"
                        );

            }

            var response = await this.lawyfirmService.CreateLawFirmAsync(lawFirmModel);

            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
            //todo
            //this.requestService.SetIsApproved();
        }
        [HttpPost("CreateLawyerAndFirmName")]
        public async Task<IActionResult> CreateLawyerAndFirmName([FromBody] CreateLawyerModel lawyerModel)
        {
            //chseck
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(lawyerModel),
                       "Invalid model"
                       );
            }
            var lawFirmId = this.lawyfirmService.GetIdByName(lawyerModel.OfficeName);
            var response = await this.lawyerService.CreateLawyerAsync(lawyerModel);
            if (response == null)
            {
                BadRequest();
            }
            var companyId = response;
            this.userService.CreateUserAsync(lawyerModel, companyId);
            var responseFiorm = await this.lawyerService.AddLawyerToLawFirmAsync(companyId, lawFirmId);

            return Ok(response);
        }
        [HttpGet("GetAllRequests")]
        public async Task<IEnumerable<RequestViewModel>> GetAllRequests()
        {
            var allRequests = this.requestService.GetAllRequests<RequestViewModel>();

            return allRequests;

        }

        [HttpGet("GetAllLawyers")]
        public IEnumerable<AllLawyersAdministrationViewModel> GetAllLawyers()
        {
            var lawyers = this.lawyerService.GetAllLawyers<AllLawyersAdministrationViewModel>();

            return lawyers;

        }

        [HttpGet("GetAllLawFirms")]
        public IEnumerable<LawFirmAdministrationViewModel> GetAllLawFirms()
        {
            var lawFirms = this.lawyfirmService.GetAll<LawFirmAdministrationViewModel>();

            return lawFirms;
        }

        [HttpGet("GetAllNotary")]
        public IEnumerable<AllNotaryAdministrationViewModel> GetAllNotary()
        {
            var notary = this.notaryService.GetAllNotary<AllNotaryAdministrationViewModel>();

            return notary;
        }

        [HttpPut("EditNotary")]
        public async Task<IActionResult> EditNotary([FromBody]EditNotaryModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.notaryService.EditNotaryByAdministratorAsync(model);
                return Ok();
            }
            return BadRequest();
           
        }

        [HttpPut("EditLawFirm")]
        public async Task<IActionResult> EditLawFirm([FromBody] EditLawFirmAdministrationModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.lawyfirmService.EditLawFirmAsync(model);
                return Ok();
            }
            return BadRequest();

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

        [HttpGet("GetAllUsers")]
        public IEnumerable<AllUsersAdministatorViewModel> GetAllUsers()
        {
            var result = this.userService.GetAll<AllUsersAdministatorViewModel>();

            return result;
        }
        [HttpPut("EditLawyer")]
        public async Task<IActionResult> EditLawyer([FromBody]EditLawyerModel? inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.lawyerService.EditLawyerByAdministratorAsync(inputModel);
                return Ok();
            }
           
           
            return BadRequest();
        }
        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UserEditModel? inputModel)
        {
           
            if (this.ModelState.IsValid)
            {
                await this.userService.EditUserAsync(inputModel);
                return Ok();
            }
            return BadRequest();
            
        }
    }
}
