using LaweyrServices.Web.Shared.AdministratioInputModels;
using LaweyrServices.Web.Shared.AministrationViewModels;
using LaweyrServices.Web.Shared.CourtModels;
using LaweyrServices.Web.Shared.LawFirmModels;
using LaweyrServices.Web.Shared.ModeratorModels;
using LaweyrServices.Web.Shared.NotaryModels;
using LaweyrServices.Web.Shared.UserModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ICompanyService companyService;
        private readonly IRatingService ratingService;
        private readonly IWorkingTimeExceptionService workingTimeExceptionService;
        private readonly IModeratorService moderatorService;
        private readonly IRequestsService requestsService;
        private readonly ICourtService courtService;
        public AdministratorController(
            IImageService imageService, ITownService townService,
            IRequestsService requestService,
            ILawyerService lawyerService, IWorkingTimeExceptionService wteService, INotaryService notaryService, IUserService userService, ILawFirmService lawyfirmService, ICompanyService companyService, IRatingService ratingService, IWorkingTimeExceptionService workingTimeExceptionService, IModeratorService moderatorService, IRequestsService requestsService, ICourtService courtService)
        {
            this.townService = townService;
            this.requestService = requestService;
            this.userService = userService;
            this.lawyerService = lawyerService;
            this.notaryService = notaryService;
            this.userService = userService;
            this.lawyfirmService = lawyfirmService;
            this.companyService = companyService;
            this.ratingService = ratingService;
            this.workingTimeExceptionService = workingTimeExceptionService;
            this.moderatorService = moderatorService;
            this.requestsService = requestsService;
            this.courtService = courtService;
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
        }
        [HttpPost("CreateCourt")]
        public async Task<IActionResult> CreateCourt([FromBody] CourtInputModel courtInputModel)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(courtInputModel),
                        "Invalid model"
                        );

            }
            await this.courtService.CreateCourt(courtInputModel);
            return Ok();
        }
        [HttpPost("FindByPhoneAsync")]
        public async Task<IActionResult> FindByPhoneAsync([FromBody]string? phone)
        {

            if (String.IsNullOrEmpty(phone))
            {
                return BadRequest();
            }

            var response = await this.lawyerService.ExistingLawyerByPhoneAsync(phone);
            if (response)
            {
                return BadRequest();
            }
           
            return Ok();
        }
        [HttpPost("ExistingEmailAsync")]
        public async Task<IActionResult> ExistingEmailAsync([FromBody] string? email)
        {

            if (String.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            var response = await this.lawyerService.ExistingLawyerByEmail(email);
            if (response)
            {
                return BadRequest();
            }

            return Ok();
        }
        [HttpGet("ExistEmailOrPhone")]
        public async Task<ExistingEmailOrPhoneModel> ExistEmailOrPhone(string email, string phoneNumber)
        {
            var model = new ExistingEmailOrPhoneModel();
            model.PhoneNumber = await this.userService.ExistingPhoneNumber(phoneNumber);
            model.Email = await this.userService.ExistingEmailAddress(email);

            return model;
        }
        [HttpPost("CreateNotary")]
        public async Task<IActionResult> CreateNotary([FromBody] CreateNotaryModel notaryModel)
        {
            var existigPhone = await this.userService.ExistingPhoneNumber(notaryModel.PhoneNumber);
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
                return BadRequest();
            }
            var companyId = response;
            this.userService.CreateNotaryUserAsync(notaryModel, companyId);
            return Ok();

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

        }
        [HttpPost("CreateModerator")]
        public async Task<IActionResult> CreateModerator([FromBody] ModeratorInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await this.moderatorService.CreateModerator(model);
            return Ok();
        }
        [HttpPost("ApprovedRequest")]
        public async Task<IActionResult> ApprovedRequest([FromBody]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await this.requestsService.SetIsApprovedAsync(id);
            return Ok();
        }
        [HttpPost("CreateLawyerAndFirmName")]
        public async Task<IActionResult> CreateLawyerAndFirmName([FromBody] CreateLawyerModel lawyerModel)
        {
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
            var lawyers = this.lawyerService.GetAllLawyersByAdministrator<AllLawyersAdministrationViewModel>();

            return lawyers;

        }
        [HttpGet("GetAllModerators")]
        public async Task<IEnumerable<ModeratorViewModel>> GetAllModerators()
        {
           var moderators = await this.moderatorService.GetAllModerators<ModeratorViewModel>();

            return moderators;

        }
        [HttpGet("GetDashboardInformation")]
        public async Task<AdminDashboardViewModel> GetDashboardInformation()
        {
            var model = new AdminDashboardViewModel();
            model.Ratings = await this.ratingService.GetAllModerateRatingsAsync();
            model.NotaryCount = await this.companyService.GetNotaryCountAsync();
            model.LawyersCount = await this.companyService.GetLawyersCountAsync();
            model.UsersCount = await this.userService.GetUsersCountAsync();
            model.AppointmentCount = await this.workingTimeExceptionService.GetAppointmentsCountAsync();
    
            return model;
        }
        [HttpGet("GetAllLawFirms")]
        public async Task<IEnumerable<LawFirmAdministrationViewModel>> GetAllLawFirms()
        {
            var lawFirms = await this.lawyfirmService.GetAll<LawFirmAdministrationViewModel>();

            return lawFirms;
        }

        [HttpGet("GetAllNotary")]
        public IEnumerable<AllNotaryAdministrationViewModel> GetAllNotary()
        {
            var notary = this.notaryService.GetAllNotaryByAdministrator<AllNotaryAdministrationViewModel>();

            return notary;
        }

        [HttpPut("EditNotary")]
        public async Task<IActionResult> EditNotary([FromBody] EditNotaryModel model)
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
        public async Task<IEnumerable<TownViewModel>> GetTowns()
        {
            var towns = await this.townService.GetAll<TownViewModel>();

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
        public async Task<IActionResult> EditLawyer([FromBody] EditLawyerModel? inputModel)
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
        [HttpPut("StopAccount")]
        public async Task<IActionResult> StopAccount([FromBody] string Id)
        {

            if (Id != null)
            {
                await this.companyService.StopAccountAsync(Id);
                return Ok();
            }
            return BadRequest();

        }
        [HttpPut("ActivateAccount")]
        public async Task<IActionResult> ActivateAccount([FromBody] string Id)
        {

            if (Id != null)
            {
                await this.companyService.ActivateAccountAsync(Id);
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete("DeleteLayerAccount")]
        public async Task<IActionResult> DeleteLayerAccount([FromQuery] string Id)
        {

            if (Id != null)
            {
                await this.lawyerService.DeleteLawyer(Id);
                return Ok();
            }
            return BadRequest();

        }
        [HttpPut("RestoreLayerAccount")]
        public async Task<IActionResult> RestoreLayerAccount([FromBody] string Id)
        {

            if (Id != null)
            {
                await this.lawyerService.RestoreAccount(Id);
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete("DeleteNotaryAccount")]
        public async Task<IActionResult> DeleteNotaryAccount([FromQuery] string Id)
        {

            if (Id != null)
            {
                await this.notaryService.DeleteNotary(Id);
                return Ok();
            }
            return BadRequest();

        }
        [HttpPut("RestoreNotaryAccount")]
        public async Task<IActionResult> RestoreNotaryAccount([FromBody] string Id)
        {

            if (Id != null)
            {
                await this.notaryService.RestoreAccount(Id);
                return Ok();
            }
            return BadRequest();

        }
        [HttpPut("CenzoredReview")]
        public async Task<IActionResult> CenzoredReview([FromBody] string reviewId)
        {
            if (!String.IsNullOrEmpty(reviewId))
            {
                var response = await this.ratingService.CensoredRatingReviewAsync(reviewId);
                if (!response)
                {
                    return BadRequest();
                }
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("ModerateReview")]
        public async Task<IActionResult> ModerateReview([FromBody] string reviewId)
        {
            if (!String.IsNullOrEmpty(reviewId))
            {
                await this.ratingService.ModerateRatingAsync(reviewId);
                
                return Ok();
            }
            return BadRequest();
        }
    }
}
