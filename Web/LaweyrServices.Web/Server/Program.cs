using IdentityModel;
using LawyerServices.Data;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Data.Seeding;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using LawyerServices.Services.Mapping;
using LawyerServices.Web.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Radzen;
using Syncfusion.Blazor;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    const string OpenId = "openid";

                    options.IdentityResources[OpenId].UserClaims.Add(JwtClaimTypes.Email);
                    options.ApiResources.Single().UserClaims.Add(JwtClaimTypes.Email);

                    options.IdentityResources[OpenId].UserClaims.Add(JwtClaimTypes.Role);
                    options.ApiResources.Single().UserClaims.Add(JwtClaimTypes.Role);
                });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(JwtClaimTypes.Role);

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Uncomment as you configure Cloudinary in appsettings.json

//var account = new Account
//           (
//               builder.Configuration["Cloudinary:AppName"],
//               builder.Configuration["Cloudinary:AppKey"],
//               builder.Configuration["Cloudinary:AppSecret"]
//      );

//Cloudinary cloudinary = new Cloudinary(account);
//cloudinary.Api.Secure = true;

//Application services
//Application services
builder.Services.AddTransient<ITownService, TownService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<ISubmitCompanyService, SubmitCompanyService>();
builder.Services.AddTransient<ILawyerService, LawyerService>();
builder.Services.AddTransient<IAreasOfActivityService, AreasOfActivityService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<ISearchService, SearchService>();
builder.Services.AddTransient<IWorkingModelService, WorkingModelService>();
builder.Services.AddTransient<IWorkingTimeExceptionService, WorkingTimeExceptionService>();
builder.Services.AddTransient<IFixedPriceService, FixedPriceService>();
builder.Services.AddTransient<IRatingService, RatingService>();
builder.Services.AddTransient<IDateTmeManipulatorService, DateTmeManipulatorService>();
builder.Services.AddTransient<INotaryService, NotaryService>();
builder.Services.AddTransient<ILawFirmService, LawFirmService>();
//AdministrationServices
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRequestsService, RequestsService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<ICurrentProfileService, CurrentProfileService>();
builder.Services.AddHostedService<TimedHostedService>();
//Uncomment as you type SendGridApiKey in appsettings.json

//builder.Services.AddTransient<IEmailSender>(
//                serviceProvider => new SendGridEmailSender(builder.Configuration["SendGrid:ApiKey"]));


// Data repositories
builder.Services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IDbQueryRunner, DbQueryRunner>();

//Radzen
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
//    options.LoginPath = "/Identity/Account/Login";
//    options.SlidingExpiration = true;
//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var serviceScope = app.Services.CreateScope())
{
    AutoMapperConfiguration.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    IServiceProvider serviceProvider = serviceScope.ServiceProvider;
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    ApplicationDbInitialiser.SeedRoles(roleManager);
    ApplicationDbInitialiser.SeedUsers(userManager);
    new ApplicationSeeder().SeedAsync(dbContext, serviceProvider).GetAwaiter().GetResult();

}
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

//app.UseCors(policy =>
//    policy.WithOrigins("http://localhost:5000", "https://maps.google.com/")
//    .AllowAnyMethod()
//    .WithHeaders(HeaderNames.ContentType));


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();