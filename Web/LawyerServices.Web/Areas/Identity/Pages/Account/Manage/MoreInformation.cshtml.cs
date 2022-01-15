using LawyerServices.Common.LawyerViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Web.Areas.Identity.Pages.Account.Manage
{
    public class MoreInformationModel : PageModel
    {
        [BindProperty]
        [Required]
        public string? Languages { get; set; }
        [BindProperty]
        [Required]
        public string? Education { get; set; }
        [BindProperty]
        [Required]
        public string? Qualifications { get; set; }
        [BindProperty]
        [Required]
        public string Experience { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public MoreInformationViewModel moreInformation { get; set; }

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompanyService companyService;

        public MoreInformationModel(ICompanyService companyService, UserManager<ApplicationUser> userManager)
        {
            this.companyService = companyService;
            this.userManager = userManager;
        }

        public async void OnGet()
        {
            var user = userManager.GetUserAsync(this.User).Result;
            var companyId = user.CompanyId;
            this.moreInformation = await this.companyService.GetMoreInformation(companyId);
            if (moreInformation != null)
            {
                Languages = moreInformation.Languages;
                Qualifications = moreInformation.Qualifications;
                Experience = moreInformation.Experience;
                Education = moreInformation.Education;
            }
             
        }

        public void OnPost(string languages, string education, string qualifications, string experience)
        {
            var user = userManager.GetUserAsync(this.User).Result;
            var companyId = user.CompanyId;
            var userId = userManager.GetUserId(this.User);
            this.companyService.CreateMoreInformation(languages,education,qualifications, experience, companyId);
        }
    }
}
