using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IdentityServer8.Models;

using Serilog;

using asbEvent.Data;
using asbEvent.Interfaces;
using asbEvent.Repositories;
using asbEvent.Services;
using asbEvent.Helpers;
using asbEvent.Models;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Configure IdentityServer
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryClients(new List<Client>
    {
        new Client
        {
            ClientId = "m2m_client",
            ClientSecrets = { new Secret("m2m_secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = { "my_api" }
        },
        new Client
        {
            ClientId = "user_client",
            ClientSecrets = { new Secret("user_client_secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = { "https://localhost:5072/signin-oidc" },
            PostLogoutRedirectUris = { "https://localhost:5072/signout-callback-oidc" },
            AllowedScopes = { "openid", "profile", "my_api" }
        }
    })
    .AddInMemoryApiScopes(new List<ApiScope>
    {
        new ApiScope("my_api", "My API")
    })
    .AddInMemoryIdentityResources(new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    })
    .AddDeveloperSigningCredential(); // Use for dev only

// Add JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://localhost:5072";
    options.ClientId = "user_client";
    options.ClientSecret = "user_client_secret";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("my_api");
    options.GetClaimsFromUserInfoEndpoint = true;
});

builder.Services.AddAuthorization();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Host.UseSerilog((context, services, configuration) =>
    {
        // Reads configuration settings for Serilog from the appsettings.json file or any other configuration source
        // This enables setting options such as log levels, sinks, and output formats directly from configuration files.
        configuration.ReadFrom.Configuration(context.Configuration);
    });

//Add repositories to the container
builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped<AttendeeRepository>();
builder.Services.AddScoped<EventRegistrationRepository>();

// Add services to the container.
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddSingleton<Emailer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
