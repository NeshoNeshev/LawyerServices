# Pravenpotal.com

Pravenportal.com is a platform for booking appointments with lawyers, law firms and notaries, as well as managing schedules with court hearings and deadlines by a lawyer or office administrator, appointment management for lawyers.

## Framework  and Language version
-	Dot Net 6.0
-	C# 10

## Technologies
    Front end and Back end
-	Blazor WASM 
-   HTML
-   CSS
-	Asp Net Core Web Api
-	ML Dotnet
## Author

- [Nesho Neshev]( https://github.com/NeshoNeshev)

## Project Overview

-	Project dependency

![Dependencies Graph](https://github.com/NeshoNeshev/LawyerServices/blob/master/PravenportalDiagram.jpg)

-	Project structure

![image](https://github.com/NeshoNeshev/LawyerServices/blob/master/PravenportalSnapShot.jpg)

### Common

** LawyerServices.Common** contains global things. For example:

- [GlobalConstants.cs](https://github.com/NeshoNeshev/LawyerServices/blob/master/LawyerServices.Common/GlobalConstants.cs).

### Data

This solution folder contains two subfolders:

- LawyerServices.Data.Data
- LawyerServices.Data.Models

#### LawyerServices.Data

[LawyerServices.Data](https://github.com/NeshoNeshev/LawyerServices/tree/master/Data/LawyerServices.Data) contains DbContext, Migrations and Configuraitons for the EF Core and Seeding users and roles.

#### LawyerServices.Data.Models

[BlazorWebAssembly.Data.Models](https://github.com/NeshoNeshev/LawyerServices/tree/master/Data/LawyerServices.Data.Models) contains backend models, Deletable Models ApplicationUser and ApplicationRole classes and interfaces.


### Services

This solution folder contains three subfolders:

- LawyerServices.Services.Data
- LawyerServices.Services.Mapping
- LawyerServices.Services.Messaging

#### LawyerServices.Services

[LawyerServices.Services.Data](https://github.com/NeshoNeshev/LawyerServices/tree/master/Services) wil contains service layer and logic.
- LawyerServices.Services.Data contains many services, interfaces and one subfolder:

- - CompanyService and ICompanyService interface
- - CountryService and ICountryService interface
- - CourtService and ICourtService interface
- - CurrentProfileService and ICurrentProfileService interface
- - DateTmeManipulatorService and IDateTmeManipulatorService interface
- - FixedPriceService and IFixedPriceService interface
- - HtmlParser and IHtmlParser interface
- - ImageService and IImageService interface
- - LawFirmService and ILawFirmService interface
- - LawyerScheduleService and ILawyerScheduleService interface
- - RatingService and IRatingService interface
- - SearchService and ISearchService interface
- - SubmitCompanyService and ISubmitCompanyService interface
- - TimeService and ITimeService interface
- - TownService and ITownService interface
- - WorkingModelService and IWorkingModelService interface
- - WorkingTimeExceptionService and IWorkingTimeExceptionService interface

- Subfolder AdminServices this subfolder contains administration services:
- - EventService and IEventService interface
- - LawyerService and ILawyerService interface
- - LocationService and ILocationService interface
- - ModeratorService and IModeratorService interface
- - NotaryService and INotaryService interface
- - RequestsService and IRequestsService interface
- - SmsService and ISmsService interface
- - TimedHostedService and ITimedHostedService interface
- - UserService and IUserService interface

#### LawyerServices.Services.Mapping

[LawyerServices.Services.Mapping](https://github.com/NeshoNeshev/LawyerServices/tree/master/Services/LawyerServices.Services.Mapping) provides simplified functionlity for auto mapping. For example:

```csharp
using BlazorWebAssembly.Services.Mapping.Interfaces;
using BlazorWebAssembly.Data.Models.DemoModels;

namespace BlazorWebAssembly.Web.Shared
{
    public class DemoViewModel : IMapFrom<Demo>
    {
        public string Name { get; set; }
    }
} 
```

#### BlazorWebAssembly.Services.Messaging

[LawyerServices.Services.Messaging](https://github.com/NeshoNeshev/LawyerServices/tree/master/Services/LawyerServices.Services.Messaging) a ready to use integration with [SendGrid](https://sendgrid.com/).
### Tests

This solution folder contains three subfolders:

- BlazorWebAssembly.Test.Web
- BlazorWebAssembly.Services.Data.Tests

#### LawyerServices.Web.Tests

[LawyerServices.Test.Webs](https://github.com/NeshoNeshev/LawyerServices/tree/master/Tests/Lawyer.Services.Test) setted up Bunit tests.

### Web

This solution folder contains three subfolders:

- LawyerServices.Web.Client
- LawyerServices.Web.Server
- LawyerServices.Web.Shared

#### BlazorWebAssembly.Web.Client

[BlazorWebAssembly.Web.Client](https://github.com/NeshoNeshev/LawyerServices/tree/master/Web/LaweyrServices.Web/Client) contains Blazor Client side functionality and pages.
## Pages
- [About](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/About.razor)
- [Authentication](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Authentication.razor)
- [Booking](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Booking.razor)
- [Contact](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Contact.razor)
- [Faq](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Faq.razor)
- [Feedback](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Feedback.razor)
- [HelpDesk](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/HelpDesk.razor)
- [History](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/History.razor)
- [Index](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Index.razor)
- [LawFirm](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/LawFirm.razor)
```cshar
```

#### BlazorWebAssembly.Web.Server

[BlazorWebAssembly.Web.Server](https://github.com/NeshoNeshev/BlazorWebAssembly-Template/tree/master/Web/BlazorWebAssembly.Web/Server) contains controlllers, areas and server side functionality.
## Support
-	Attribute [Authorize(Roles = "Administrator")] For Example:

```csharp
using BlazaorWebAssembly.Services.Interfaces;
using BlazorWebAssembly.Web.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly.Web.Server.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class DemoAdministrationController : ControllerBase
    {
        private readonly IDemoService demoService;

        public DemoAdministrationController(IDemoService demoService)
        {
            this.demoService = demoService;
        }
        [HttpGet]
        public DemoViewModel Get()
        {
            return this.demoService.GetDemo("FirstDemo");
        }

        [HttpPost]
        public IActionResult Post([FromBody] string? name)
        {
            var demo = this.demoService.CreateDemo(name);
            if (demo != null)
            {
                return Ok(demo);
            }
            return BadRequest("Existing Demo");
        }
    }
}
```

#### BlazorWebAssembly.Web.Shared

[BlazorWebAssembly.Web.Shared](https://github.com/NeshoNeshev/BlazorWebAssembly-Template/tree/master/Web/BlazorWebAssembly.Web/Shared) contains view models that are mapped from backend to fronted. 

## Support


If you are having problems, please let me know by [raising a new issue](https://github.com/NeshoNeshev/BlazorWebAssembly-Template/issues).


## License

This project is licensed with the [MIT license](LICENSE).
