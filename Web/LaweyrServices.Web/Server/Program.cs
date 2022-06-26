using IdentityModel;
using LaweyrServices.Web.Server;
using LawyerServices.Data;
using LawyerServices.Data.Models;
using LawyerServices.Data.Repositories;
using LawyerServices.Data.Seeding;
using LawyerServices.Services.Data;
using LawyerServices.Services.Data.AdminServices;
using LawyerServices.Services.Data.AdminServices.AreasOfActivityServices;
using LawyerServices.Services.Mapping;
using LawyerServices.Services.Messaging;
using LawyerServices.Web.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
//services.AddIdentityServer()
//    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(opt =>
//    {
//        opt.IdentityResources["openid"].UserClaims.Add("role");
//        opt.ApiResources.Single().UserClaims.Add("role");
//    });
//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
builder.Services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    options.IdentityResources["openid"].UserClaims.Add("role");
                    options.ApiResources.Single().UserClaims.Add("role");
                });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
builder.Services.AddApiAuthorization()
    .AddAccountClaimsPrincipalFactory<CustomUserFactory>();
builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRequestsService, RequestsService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<ICurrentProfileService, CurrentProfileService>();
builder.Services.AddHostedService<TimedHostedService>();
builder.Services.AddTransient<ISmsService, SmsService>();
builder.Services.AddTransient<ITimeService, TimeService>();
builder.Services.AddTransient<IModeratorService, ModeratorService>();

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
//SendGrid
builder.Services.AddTransient<IEmailSender, NullMessageSender>();
builder.Services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(builder.Configuration["SendGrid:ApiKey"]));
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
    new ApplicationSeeder().SeedAsync(dbContext, serviceProvider).GetAwaiter().GetResult();

}
 void Configure(IApplicationBuilder app)
{

    app.Use(next => new RequestDelegate(
        async context =>
        {
            context.Request.EnableBuffering();
            await next(context);
        }
    ));

    // YOUR OTHER CONFIG...
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