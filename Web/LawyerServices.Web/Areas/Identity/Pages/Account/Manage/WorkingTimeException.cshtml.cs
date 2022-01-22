using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Web.Areas.Identity.Pages.Account.Manage
{
    public class WorkingTimeExceptionModel : PageModel
    {

        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [BindProperty]
        public DateTime Time  { get; set; }


        public WorkingTimeExceptionModel()
        {

        }
        public void OnGet()
        {
           
        }
        public void OnPost(DateTime? date, double time)
        {
        }

    }
}
