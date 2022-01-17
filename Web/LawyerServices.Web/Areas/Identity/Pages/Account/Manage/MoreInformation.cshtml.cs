using LawyerServices.Common.LawyerViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;

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
        public string? Experience { get; set; }

        [BindProperty]
        public string? Website { get; set; }


        //todo: not work corect!!!!
        [BindProperty]
        public IFormFile Upload { get; set; }


        [TempData]
        public string? StatusMessage { get; set; }

        public MoreInformationViewModel moreInformation { get; set; }

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompanyService companyService;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        [Obsolete]
        public MoreInformationModel(ICompanyService companyService, UserManager<ApplicationUser> userManager, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            this.companyService = companyService;
            this.userManager = userManager;
            _environment = environment;
        }

        public async void OnGet()
        {
            var user = userManager.GetUserAsync(this.User).Result;
            var companyId = user.CompanyId;
            this.moreInformation =  this.companyService.GetMoreInformation(companyId);
            if (moreInformation != null)
            {
                Languages = moreInformation.Languages;
                Qualifications = moreInformation.Qualifications;
                Experience = moreInformation.Experience;
                Education = moreInformation.Education;
            }
             
        }

        public async Task<IActionResult> OnPost(string languages, string education, string qualifications, string experience,string? website, IFormFile? Upload)
        {
            var user = userManager.GetUserAsync(this.User).Result;
            var companyId = user.CompanyId;
            //var userId = userManager.GetUserId(this.User);
            if (Upload == null)
            {
                return Page();
            }
            var file = Path.Combine(_environment.ContentRootPath, "wwwroot/images", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            if (!this.ModelState.IsValid)
            {
                return Page();
            }

            var a = this.companyService.CreateMoreInformation(languages,education,qualifications, experience, website, companyId, file);
    
            return RedirectToPage();
        }
    }
}
