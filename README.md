# Pravenpotal.com

Pravenportal.com is a platform for booking an appointment with a lawyer as well as managing court hearing schedules, managing lawyer deadlines.

## Framework  and Language version
-	Dot Net 6.0
-	C# 10

## Technologies
-	Blazor WASM ASP Net Hosted
-	Web Api
-   HTML
-   CSS


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

[BlazorWebAssembly.Data](https://github.com/NeshoNeshev/LawyerServices/tree/master/Data/LawyerServices.Data) contains DbContext, Migrations and Configuraitons for the EF Core and Seeding users and roles.

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

[LawyerServices.Services.Messaging](https://github.com/NeshoNeshev/BlazorWebAssembly-Template/tree/master/Services/BlazorWebAssembly.Services.Messaging) a ready to use integration with [SendGrid](https://sendgrid.com/).
### Tests

This solution folder contains three subfolders:

- BlazorWebAssembly.Test.Web
- BlazorWebAssembly.Services.Data.Tests

#### LawyerServices.Web.Tests

[LawyerServices.Test.Webs](https://github.com/NeshoNeshev/BlazorWebAssembly-Template/tree/master/Tests/BlazaorWebAssembly.Test.Web) setted up Bunit tests.

### Web

This solution folder contains three subfolders:

- BlazorWebAssembly.Web.Client
- BlazorWebAssembly.Web.Server
- BlazorWebAssembly.Web.Shared

#### BlazorWebAssembly.Web.Client

[BlazorWebAssembly.Web.Client](https://github.com/NeshoNeshev/BlazorWebAssembly-Template/tree/master/Web/BlazorWebAssembly.Web/Client) contains Blazor Client side functionality.
## Support
-	Not authenticated client to return data from controller. For Example:
```csharp
@code {

    private DemoViewModel? viewModel;

    protected override async Task OnInitializedAsync()
    {
        //This component demonstrate how to return data from controller with No Authentication Client.
        var client = ClientFactory.CreateClient("BlazorWebAssembly.Web.ServerAPI.NoAuthenticationClient");
        try
        {
            viewModel = await client.GetFromJsonAsync<DemoViewModel>("Index");
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
        base.OnInitialized();
    }
}
```

-	Role based authorization. For Example:

```csharp
@page "/administration"
@using BlazorWebAssembly.Web.Shared
@using Microsoft.AspNetCore.Authorization
@using Microsoft.AspNetCore.Components.WebAssembly.Authentication
@using System.ComponentModel.DataAnnotations

@attribute [Authorize(Roles = "Administrator")]
@inject HttpClient Http

<PageTitle>Administration</PageTitle>

<h1 class= "text-center">Administration</h1>
<AuthorizeView Roles="Administrator">
    <Authorized>
        <h1>Hello, @context.User.Identity.Name!</h1>
        <p>You can only see this content if you're Administrator.</p>

    </Authorized>
</AuthorizeView>

<AuthorizeView Roles="Moderator">
    <Authorized>
        <h1>Hello, @context.User.Identity.Name!</h1>
        <p>You can only see this content if you're Moderator.</p>

    </Authorized>
</AuthorizeView>

@if (viewModel == null)
{
    
    <p><em>Loading...</em></p>
}
else
{   
    <h4 class = "text-center">This is response on Administration Controller => @viewModel.Name</h4>

}
<div class="card text-center">
  <div class="card-header">
   Post only is in role Administrator
  </div>
  <div class="card-body">
    
    <EditForm Model="@inputModel" OnValidSubmit="@HandleValidSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />

    <p>
        <label>
            Name:
            <InputText @bind-Value="inputModel.Name" />
        </label>
    </p>
    <button type="submit">Submit</button>
</EditForm>
  </div>
  <div class="card-footer text-muted">
    @DateTime.UtcNow.Date.Year
  </div>
</div>


@code {
    private DemoViewModel? viewModel;

    private DemoInputModel? inputModel;

    protected override async Task OnInitializedAsync()
    {
        inputModel = new DemoInputModel();

        try
        {
            viewModel = await Http.GetFromJsonAsync<DemoViewModel>("DemoAdministration");
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }
    private void HandleValidSubmit()
    {
        var response = Http.PostAsJsonAsync("DemoAdministration", inputModel?.Name);
    }
}
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

