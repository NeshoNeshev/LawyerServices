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

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyService companyService;

        public MoreInformationModel()
        {

        }

        public void OnGet()
        {
        }

        public void OnPost(string languages, string education, string qualifications, string experience)
        {
            var user = _userManager.GetUserAsync(this.User).Result;
            var companyId = user.CompanyId;
            var userId = _userManager.GetUserId(this.User);
        }
    }
}
