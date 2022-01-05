using LawyerServices.Common.AreasOfActivityViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
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
        public InputModel Input { get; set; }

        public IEnumerable<AreasOfActivityViewModel> AreasOfActivitys { get; set; }

        private IAreasOfActivityService areasOfActivityService;

        public LawyerAreaOfActivitiesModel(IEnumerable<AreasOfActivityViewModel> areasOfActivitys, IAreasOfActivityService areasOfActivityService = null)
        {
            AreasOfActivitys = areasOfActivitys;
            this.areasOfActivityService = areasOfActivityService;
        }


        public void OnGet()
        {
            this.AreasOfActivitys = this.areasOfActivityService.GetAll<AreasOfActivityViewModel>();
        }
        public void OnPOst(InputModel model)
        {
            Console.WriteLine("");
        }
    }
    public class InputModel
    {

        [Display(Name = "Copyright")]
        public string Copyright { get; set; }
    }
}
