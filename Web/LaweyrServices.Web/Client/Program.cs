using LaweyrServices.Web.Client;
using LaweyrServices.Web.Client.ClientServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("LaweyrServices.Web.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


builder.Services.AddHttpClient("LaweyrServices.Web.ServerAPI.NoAuthenticationClient",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("LaweyrServices.Web.ServerAPI"));
builder.Services.AddScoped(typeof(AccountClaimsPrincipalFactory<RemoteUserAccount>), typeof(RolesAccountClaimsPrincipalFactory));

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddTransient<ICreateProfileImage, CreateProfileImage>();

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
