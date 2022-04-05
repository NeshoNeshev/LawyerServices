﻿using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.AreasOfActivityViewModels;
using LaweyrServices.Web.Shared.DateModels;
using LaweyrServices.Web.Shared.FixedCostModels;
using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.UserModels;
using LaweyrServices.Web.Shared.WorkingTimeModels;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        private readonly IImageService imageService;
        private readonly IFixedPriceService fixedPriceService;

        public CompanyController(ITownService townService, IAreasOfActivityService areaService, ISearchService searchService, ILawyerService lawyerService, IWorkingTimeExceptionService wteService, ICompanyService companyService, IImageService imageService, IFixedPriceService fixedPriceService)
        {
            this.townService = townService;
            this.areaService = areaService;
            this.searchService = searchService;
            this.lawyerService = lawyerService;
            this.wteService = wteService;
            this.companyService = companyService;
            this.imageService = imageService;
            this.fixedPriceService = fixedPriceService;
        }
        
        [HttpGet("WteCount")]
        public int WteCount()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var count = this.wteService.GetRequstsCount(userId);

            return count;
        }

        [HttpGet("Search")]
        public IEnumerable<LawyerListItem> Search( string? name, string? town, string? area)
        {
            var lawyers =  this.searchService.Search(name, town, area).Result;

            return lawyers;
        }

       
        [HttpGet("Count")]
        public async Task<int> Count( string? name, string? town, string? area)
        {
            var count =  this.searchService.Search(name, town, area).Result.Count();

            return count;
        }
        [HttpGet("GetTowns")]
        public IEnumerable<TownViewModel> GetTowns()
        {
            var towns = this.townService.GetAll<TownViewModel>();

            return towns;
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

        [HttpGet("GetAreas")]
        public IEnumerable<AreasOfActivityViewModel> GetAreas()
        {
            var areas = this.areaService.GetAll<AreasOfActivityViewModel>();
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
        public List<WorkingTimeExceptionBookingModel> GetAllRequsts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response  = this.wteService.GetAllRequsts(userId).ToList();

            return response;
        }

        [HttpGet("GetMoreInformation")]
        public  MoreInformationInputModel? GetMoreInformation()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var response =  this.companyService.GetMoreInformation(userId);

            return response;
        }
       
        [HttpPost("EditImage")]
        public void EditImage(string name,byte[] bytes)
        {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string extension;
            extension = name.Substring(name.Length - 4);

            this.lawyerService.EditImage(bytes, userId, extension);

        }       

        [Authorize(Roles = "Lawyer")]
        [HttpPut("UpdateFeatures")]
        public IActionResult UpdateFeatures([FromBody] FeaturesInputModel? model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var lawyerId = this.companyService.GetCompanyId(userId);

            if (lawyerId == null || model == null)
            {
                return BadRequest();
            }
            else
            {
                this.companyService.UpdateFeatures(model, lawyerId);
            }

            return Ok(model); ;

        }
    }
}