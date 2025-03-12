using BlazorAppSecure;
using BlazorAppSecure.Services.Blog;
using BlazorAppSecure.Sevices;
using BlazorAppSecure.Sevices.Blog;
using BlazorAppSecure.Sevices.Category;
using BlazorAppSecure.Sevices.Profile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
if (builder.HostEnvironment.IsDevelopment()) {
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
}

builder.Services.AddAntDesign();
builder.Services.AddAuthorizationCore();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CutomHttpHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = 
    new Uri(builder.Configuration["FrontendUrl"] ??  "https://localhost:5002") });


builder.Services.AddHttpClient("Auth", opt => opt.BaseAddress =
new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7030"))
    .AddHttpMessageHandler<CutomHttpHandler>();

builder.Services.AddSingleton(sp =>
    new HubConnectionBuilder()
        .WithUrl("https://localhost:7239/commentHub", options =>
        {
            options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
        })
        .Build());


await builder.Build().RunAsync();
