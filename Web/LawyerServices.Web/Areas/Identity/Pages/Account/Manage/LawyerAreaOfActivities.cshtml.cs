using LawyerServices.Common.AreasOfActivityViewModels;
using LawyerServices.Data.Models;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LawyerServices.Web.Areas.Identity.Pages.Account.Manage
{
    public class LawyerAreaOfActivitiesModel : PageModel
    {
        [BindProperty]
        public string? Copyright { get; set; }
        [BindProperty]
        public string? Arbitration { get; set; }
        [BindProperty]
        public string? AirTransportLaw { get; set; }
        [BindProperty]
        public string Taxlaw { get; set; }
        [BindProperty]
        public string EuropeanRight { get; set; }
        [BindProperty]
        public string EthicalRules { get; set; }
        [BindProperty]
        public string Suffrage { get; set; }
        [BindProperty]
        public string PropertyLaw { get; set; }
        [BindProperty]
        public string InformationLaw { get; set; }
        [BindProperty]
        public string Concessions { get; set; }
        [BindProperty]
        public string MedicalLaw { get; set; }
        [BindProperty]
        public string MaritimeLaw { get; set; }
        [BindProperty]
        public string RealEstate { get; set; }
        [BindProperty]
        public string Procurement { get; set; }
        [BindProperty]
        public string EuropeanUnionLaw { get; set; }
        [BindProperty]
        public string ProceduralRepresentation { get; set; }
        [BindProperty]
        public string MergersAcquisitions { get; set; }
        [BindProperty]
        public string JudicialAdministrativeProceedings { get; set; }
        [BindProperty]
        public string CommercialLaw { get; set; }
        [BindProperty]
        public string FrenchLaw { get; set; }
        [BindProperty]
        public string AdministrativeLaw { get; set; }
        [BindProperty]
        public string BankingLaw { get; set; }
        [BindProperty]
        public string CivilProceedings { get; set; }
        [BindProperty]
        public string ContractLaw { get; set; }
        [BindProperty]
        public string EnvironmentalLaw { get; set; }
        [BindProperty]
        public string InsuranceLaw { get; set; }
        [BindProperty]
        public string ExecutiveProduction { get; set; }
        [BindProperty]
        public string InvestmentLaw { get; set; }
        [BindProperty]
        public string CompetitionLaw { get; set; }
        [BindProperty]
        public string CorporateLaw { get; set; }
        [BindProperty]
        public string InternationalLaw { get; set; }
        [BindProperty]
        public string CriminalLaw { get; set; }
        [BindProperty]
        public string Bankruptcy { get; set; }
        [BindProperty]
        public string AssuranceLaw { get; set; }
        [BindProperty]
        public string PrivatizationAndInvestment { get; set; }
        [BindProperty]
        public string RealEstateTransactions { get; set; }
        [BindProperty]
        public string ConstructionLaw { get; set; }
        [BindProperty]
        public string Litigation { get; set; }
        [BindProperty]
        public string PharmaceuticalLaw { get; set; }
        [BindProperty]
        public string GamblingLaw { get; set; }
        [BindProperty]
        public string AntitrustLaw { get; set; }
        [BindProperty]
        public string RealLaw { get; set; }
        [BindProperty]
        public string CivilLaw { get; set; }
        [BindProperty]
        public string CompanyLaw { get; set; }
        [BindProperty]
        public string EnergyLaw { get; set; }
        [BindProperty]
        public string Phr { get; set; }
        [BindProperty]
        public string ImmigrationLaw { get; set; }
        [BindProperty]
        public string IntellectualProperty { get; set; }
        [BindProperty]
        public string ConstitutionalRight { get; set; }
        [BindProperty]
        public string MediaLaw { get; set; }
        [BindProperty]
        public string CustomsLaw { get; set; }
        [BindProperty]
        public string InheritanceLaw { get; set; }
        [BindProperty]
        public string ContractualLaw { get; set; }
        [BindProperty]
        public string PatentLaw { get; set; }
        [BindProperty]
        public string ProceduralLaw { get; set; }
        [BindProperty]
        public string FamilyLaw { get; set; }
        [BindProperty]
        public string CollectionOfReceivables { get; set; }
        [BindProperty]
        public string LaborLaw { get; set; }
        [BindProperty]
        public string FinancialLaw { get; set; }



        [TempData]
        public string StatusMessage { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyService companyService;
        public IEnumerable<AreasOfActivityViewModel> AreasOfActivitys { get; set; }

        private IAreasOfActivityService areasOfActivityService;

        public LawyerAreaOfActivitiesModel(IEnumerable<AreasOfActivityViewModel> areasOfActivitys, IAreasOfActivityService areasOfActivityService = null, UserManager<ApplicationUser> userManager = null, ICompanyService companyService = null)
        {
            AreasOfActivitys = areasOfActivitys;
            this.areasOfActivityService = areasOfActivityService;
            _userManager = userManager;
            this.companyService = companyService;
        }


        public void OnGet()
        {

            this.AreasOfActivitys = this.areasOfActivityService.GetAll<AreasOfActivityViewModel>();
        }

        [Authorize]
        public void OnPost(string copyright)
        {
            
            var user = _userManager.GetUserAsync(this.User).Result;
            var companyId = user.CompanyId;
            var userId = _userManager.GetUserId(this.User);
            this.areasOfActivityService.CreateArea(userId, companyId, copyright);
            Console.WriteLine(Copyright + Arbitration + LaborLaw);
        }

    }

}
