using LawyerServices.Common.AreasOfActivityViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Web.Areas.Identity.Pages.Account.Manage
{
    public class LawyerAreaOfActivitiesModel : PageModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string First { get; set; }

        [BindProperty]
        public string Last { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<AreasOfActivityViewModel> AreasOfActivitys { get; set; }

        private IAreasOfActivityService areasOfActivityService;

        public LawyerAreaOfActivitiesModel(IEnumerable<AreasOfActivityViewModel> areasOfActivitys, IAreasOfActivityService areasOfActivityService = null, UserManager<ApplicationUser> userManager = null)
        {
            AreasOfActivitys = areasOfActivitys;
            this.areasOfActivityService = areasOfActivityService;
            _userManager = userManager;
        }


        public void OnGet()
        {

            this.AreasOfActivitys = this.areasOfActivityService.GetAll<AreasOfActivityViewModel>();
        }

        public void OnPost()
        {
            var user = _userManager.GetUserAsync(this.User).Result;
            var userID = _userManager.GetUserId(this.User);
            var company = user.Company;

            Console.WriteLine(First + Last);
        }

    }

}
