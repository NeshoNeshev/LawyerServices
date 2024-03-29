# Pravenpotal.com

Pravenportal.com is a platform for saving appointments in Bulgaria with lawyers, law firms and notaries, as well as managing schedules with court hearings and deadlines by a lawyer or office administrator, managing expiring deadlines for lawyers.
Management of recorded appointments in a notary's office.

## Project link
- [pravenportal.com](https://pravenportal.com/)

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

Roles

- Administrator
  - - Opportunities
  - - - Accept requests from lawyers and notarys
  - - - Create lawyer
  - - - Creating a law firm adding a lawyers to it
  - - - Create notary
  - - - Change lawyers and notarys information
  - - - Change law firms information and add lawyers to it
  - - - Block lawyers an notarys account
  - - - Delete lawyers and notarys account
  - - - Block users
  - - - Аutomatic blocking of a user in case of no-shows or canceled meetings
  - - - Create courts
  - - - Moderate review
  - Lawyer
  - - Opportunities
  - - - Change profile information
  - - - Change Law firm information if is owner
  - - - Аdd consultation hours in a time range and editing
  - - - Аdd consultation single hour and editing
  - - - Change everything in scheduler dynamically and editing
  - - - Add court meeting manually and editing
  - - - Recive reminder one day before meeting
  - - - Find and add court meeting automatically(by court, case number and year) and editing
  - - - Add another work meeting and editing
  - - - Add expiring terms and editing
  - - - Dashboard panel
  - - - Meeting panel(canceled appointment,cancel appointment in range, call from client and etc.)
  - - - Service panel(Add and edit fixed cost service, free first appointmen and area of law)
- Moderator
- - Opportunities
- - - Мanages all attorney opportunities in a given office after authorization from an attorney
- Notary
 - - - Change profile information
 - - - Мanage your own calendar with all kinds of work meetings
 - - - Meeting panel(edit appointments, call from client and etc.)
- User
- - Opportunities
- - - Booking appointments
- - - Cancel appointments
- - - Recive reminder one day before meeting
- - - Send review
- - - Search for a lawyer in a specific case (uses ML)
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

#### LawyerServices.Services.Messaging

[LawyerServices.Services.Messaging](https://github.com/NeshoNeshev/LawyerServices/tree/master/Services/LawyerServices.Services.Messaging) a ready to use integration with [SendGrid](https://sendgrid.com/).
### Tests

This solution folder contains two subfolders:

- LawyerServices.Test.Web
- LawyerServices.Services.Data.Tests

#### LawyerServices.Ml.Services
[LawyerServices.Ml.Services](https://github.com/NeshoNeshev/LawyerServices/tree/master/Ml/LawyerServices.Ml.Services) contains pretrened model using multiclass clasification algorithm 
which will indicate what kind of lawyer you need according to a specific case.

#### LawyerServices.Web.Tests

[LawyerServices.Test.Webs](https://github.com/NeshoNeshev/LawyerServices/tree/master/Tests/Lawyer.Services.Test) setted up Bunit tests.

### Web

This solution folder contains three subfolders:

- LawyerServices.Web.Client
- LawyerServices.Web.Server
- LawyerServices.Web.Shared

#### LawyerServices.Web.Client

[LawyerServices.Web.Client](https://github.com/NeshoNeshev/LawyerServices/tree/master/Web/LaweyrServices.Web/Client) contains Blazor Client side functionality and pages.
#### Pages
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
- [LawyerDetails](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/LawyerDetails.razor)
- [Lawyers](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Lawyers.razor)
- [NotAuthorized](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/NotAuthorized.razor)
- [Notary](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Notary.razor)
- [NotaryDetails](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/NotaryDetails.razor)
- [Specialtes](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Specialtes.razor)
- [SubmitАpplication](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Submit%D0%90pplication.razor)
- [SuccessedReview](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/SuccessedReview.razor)
- [SuccessfullySubmitApplication](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/SuccessfullySubmitApplication.razor)
- [TermsОfUse](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Terms%D0%9EfUse.razor)
- [ThankYou](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/ThankYou.razor)
- [Threereasons](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Threereasons.razor)
- [Towns](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Pages/Towns.razor)
#### Shared
Contains Administration pages and components

Administrations pages
- [Admin](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Admin.razor)
- [Appointments](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Appointments.razor)
- [AppointmentsByOwner](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/AppointmentsByOwner.razor)
- [Client](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Client.razor)
- [CreateCourt](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/CreateCourt.razor)
- [CreateLawFirm](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/CreateLawFirm.razor)
- [CreateNotary](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/CreateNotary.razor)
- [CreateUser](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/CreateUser.razor)
- [Dashboard](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Dashboard.razor)
- [EditLawFirm](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/EditLawFirm.razor)
- [EditSchedulerByOwner](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/EditSchedulerByOwner.razor)
- [EditServiceByOwner](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/EditServiceByOwner.razor)
- [Gdpr](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Gdpr.razor)
- [NotFound](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/NotFound.razor)
- [NotaryAppointments](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/NotaryAppointments.razor)
- [NotaryProfile](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/NotaryProfile.razor)
- [Profile](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Profile.razor)
- [Requests](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Requests.razor)
- [Service](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Service.razor)
- [SomethingWentWrong](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/SomethingWentWrong.razor)
- [UserProfile](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/UserProfile.razor)
- [AllLawFirms](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/AdministrationPages/AllLawFirms.razor)
- [AllLawyers](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/AdministrationPages/AllLawyers.razor)
- [AllModerators](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/AdministrationPages/AllModerators.razor)
- [AllNotary](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/AdministrationPages/AllNotary.razor)
- [AllUsers](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/AdministrationPages/AllUsers.razor)

Components

- [AddAdministratorToLawFirm](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/AddAdministratorToLawFirm.razor)
- [AddAppointmentPage](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/AddAppointmentPage.razor)
- [AddLawyerToLawFirmModal](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/AddLawyerToLawFirmModal.razor)
- [CancelAppoinmentByCurrentDate](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/CancelAppoinmentByCurrentDate.razor)
- [CancelAppointmentByModerator](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/CancelAppointmentByModerator.razor)
-[CancelCurrentAppointmentModal](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/CancelCurrentAppointmentModal.razor)
- [ConfirmationDateInRangeModal](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/ConfirmationDateInRangeModal.razor)
- [ConfirmationDateInRangeModalFromOwner](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/ConfirmationDateInRangeModalFromOwner.razor)
- [DetailAppointmentPage](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/DetailAppointmentPage.razor)
- [EditAppointmentPage](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/EditAppointmentPage.razor)
- [EditLawFirmByAdministrator](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/EditLawFirmByAdministrator.razor)
- [EditLawyerByAdministrator](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/EditLawyerByAdministrator.razor)
-[EditNotaryByAdministrator](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/EditNotaryByAdministrator.razor)
- [HelpCard](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/HelpCard.razor)
- [LawyerCard](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/LawyerCard.razor)
- [LawyersNotFound](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/LawyersNotFound.razor)
- [ModalFixedPrice](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/ModalFixedPrice.razor)
- [NoUpcomingLawyerHours](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/NoUpcomingLawyerHours.razor)
- [NotaryCard](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/NotaryCard.razor)
- [Scheduler](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/Scheduler%20.razor)
- [SureStopCompanyModal](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/SureStopCompanyModal.razor)
- [UserHistoryWteCard](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/UserHistoryWteCard.razor)
- [UserWteCard](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Client/Shared/Administration/Pages/Components/UserWteCard.razor)

#### LaweyrService.Web.Server

[LaweyrService.Web.Server](https://github.com/NeshoNeshev/LawyerServices/tree/master/Web/LaweyrServices.Web/Server) contains controlllers, areas, ML model, and server side functionality.
## Controllers
- [AdministratorController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/AdministratorController.cs)
- [AppointmentController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/AppointmentController.cs)
- [AreasController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/AreasController.cs)
- [BookingController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/BookingController.cs)
- [CompanyController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/CompanyController.cs)
- [ContactController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/ContactController.cs)
- [DashboardController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/DashboardController.cs)
- [EditLawFirmByOwnerController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/EditLawFirmByOwnerController.cs)
- [IndexController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/IndexController.cs)
- [LawFirmController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/LawFirmController.cs)
- [MlCaseController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/MlCaseController.cs)
- [NotaryController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/NotaryController.cs)
- [ProfileController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/ProfileController.cs)
- [ReviewController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/ReviewController.cs)
- [SchedulerController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/SchedulerController.cs)
- [ServiceController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/ServiceController.cs)
- [SubmitАpplication](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/Submit%D0%90pplicationController.cs)
- [TownsAreaController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/TownsAreaController.cs)
- [UserController](https://github.com/NeshoNeshev/LawyerServices/blob/master/Web/LaweyrServices.Web/Server/Controllers/UserController.cs)


#### LawyerServices.Web.Shared

[LawyerServices.Web.Shared](https://github.com/NeshoNeshev/LawyerServices/tree/master/Web/LaweyrServices.Web/Shared) contains view and input models that are mapped from backend to fronted. 

## Support


If you are having problems, please let me know by [raising a new issue](https://github.com/NeshoNeshev/LawyerServices/issues).


## License

This project is licensed with the [Apache License 2.0](https://github.com/NeshoNeshev/LawyerServices/blob/master/LICENSE.md).

