﻿using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.CourtModels;
using LaweyrServices.Web.Shared.FixedCostModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Treblle.Net.Core;

namespace LaweyrServices.Web.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ITownService townService;
        private readonly IAreasOfActivityService areaService;
        private readonly ISearchService searchService;
        private readonly ILawyerService lawyerService;
        private readonly IWorkingTimeExceptionService wteService;
        private readonly ICompanyService companyService;
        private readonly ITimeService timeService;
        private readonly ICourtService courtService;
        public CompanyController(ITownService townService, IAreasOfActivityService areaService, ISearchService searchService, ILawyerService lawyerService, IWorkingTimeExceptionService wteService, ICompanyService companyService, ITimeService timeService, ICourtService courtService)
        {
            this.townService = townService;
            this.areaService = areaService;
            this.searchService = searchService;
            this.lawyerService = lawyerService;
            this.wteService = wteService;
            this.companyService = companyService;
            this.timeService = timeService;
            this.courtService = courtService;
        }


        [HttpGet("GetTownAndArea")]
        public async Task<LawyerPageTownAndAreaViewModel> GetTownAndArea()
        {
            var model = new LawyerPageTownAndAreaViewModel();

            model.Towns = await this.townService.GetAll<TownViewModel>();
            model.Areas = await this.areaService.GetAll<AreasOfActivityViewModel>();

            return model;
        }
        [Treblle]
        [HttpGet("Search")]
        public async Task<ICollection<AllLawyersModel>> Search(string? name, string? town, string? area)
        {
            var lawyers = await this.searchService.SearchAsync(name, town, area);
            var date = this.timeService.GetTimeOffset(DateTime.Now);
            foreach (var item in lawyers)
            {
                if (item.WorkingTime.WorkingTimeExceptions.Any(x => x.StarFrom >= date && x.IsRequested == false && x.IsCanceled == false && x.AppointmentType == "Час за консултация"))
                {
                    string? earlyTime = item.WorkingTime.WorkingTimeExceptions.OrderBy(x => x.StarFrom).Where(x => x.StarFrom >= date && x.IsRequested == false && x.IsCanceled == false && x.AppointmentType == "Час за консултация").Select(x => x.StarFrom).First().ToString("MM/dd/yyyy HH:mm");
                    item.EarlyTime = earlyTime;
                }
               
            }
            return lawyers.ToList();
        }
        [Authorize(Roles = "Lawyer,Moderator")]
        [HttpGet("GetAllCourts")]
        public async Task<IEnumerable<CourtViewModel>> GetAllCourts()
        {
            var model = await this.courtService.GetCourtsAsync<CourtViewModel>();

            return model;
        }
        [HttpGet("GetTowns")]
        public async Task<IEnumerable<TownViewModel>> GetTowns()
        {
            var towns = await this.townService.GetAll<TownViewModel>();

            return towns;
        }

        [HttpGet("GetLawyerById")]
        public async Task<IActionResult>  GetLawyerById(string lawyerId)
        {
            var exist = this.lawyerService.ExistingLawyerById(lawyerId);
            if (!exist)
            {
                return NotFound();
            }
            var lawyer = await this.lawyerService.GetLawyerByIdAsync(lawyerId);
            return Ok(lawyer);
        }

        [HttpGet("GetAreas")]
        public async Task<IEnumerable<AreasOfActivityViewModel>> GetAreas()
        {
            var areas = await this.areaService.GetAll<AreasOfActivityViewModel>();
            return areas;
        }

        [HttpGet("FindLawyerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindLawyerById(string lawyerId)
        {
            var response = this.lawyerService.ExistingLawyerById(lawyerId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("DeleteWorkingTimeExceptionWhenDateIsOver")]
        public IActionResult DeleteWorkingTimeExceptionWhenDateIsOver()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                this.wteService.DeleteWorkingTimeExceptionWhenDateIsOver(userId);
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("GetAllRequsts")]
        public async Task<IEnumerable<WorkingTimeExceptionBookingModel>> GetAllRequsts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = await this.wteService.GetAllRequsts(userId);

            return response;
        }

        [HttpGet("GetMoreInformation")]
        public MoreInformationInputModel? GetMoreInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response = this.companyService.GetMoreInformation(userId);

            return response;
        }

        [Authorize(Roles = "Lawyer")]
        [HttpPost("EditImage")]
        public async Task<IActionResult> EditImage(string name, byte[] bytes)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string extension;
            extension = name.Substring(name.Length - 4);

           var result = await this.lawyerService.EditImageAsync(bytes, userId, extension);

            return Ok(result);

        }

        [Authorize(Roles = "Lawyer")]
        [HttpPut("UpdateFeatures")]
        public async Task<IActionResult> UpdateFeatures([FromBody] FeaturesInputModel? model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var lawyerId = await this.companyService.GetCompanyIdAsync(userId);

            if (lawyerId == null || model == null)
            {
                return BadRequest();
            }
            else
            {
               await this.companyService.UpdateFeaturesAsync(model, lawyerId);
            }

            return Ok(model); ;

        }
    }
}