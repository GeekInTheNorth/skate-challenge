namespace AllInSkateChallenge;

using System;

using AllInSkateChallenge.Features.Data;
using AllInSkateChallenge.Features.Data.Entities;
using AllInSkateChallenge.Features.Data.Static;
using AllInSkateChallenge.Features.Error;
using AllInSkateChallenge.Features.EventDetails;
using AllInSkateChallenge.Features.Framework.Routing;
using AllInSkateChallenge.Features.Gravatar;
using AllInSkateChallenge.Features.Home;
using AllInSkateChallenge.Features.Services.BlobStorage;
using AllInSkateChallenge.Features.Services.Email;
using AllInSkateChallenge.Features.Skater.Progress;
using AllInSkateChallenge.Features.Skater.SkateLog;
using AllInSkateChallenge.Features.Skater.StravaImport;
using AllInSkateChallenge.Features.Statistics;
using AllInSkateChallenge.Features.Statistics.Leaders;
using AllInSkateChallenge.Features.Strava;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication()
                .AddStrava(options => 
                { 
                    options.ClientId = Configuration["Strava:ClientId"]; 
                    options.ClientSecret = Configuration["Strava:ClientSecret"]; 
                    options.SaveTokens = true;
                    options.Scope.Add("read");
                    options.Scope.Add("activity:read");
                    options.Scope.Add("activity:read_all");
                });

        services.AddControllersWithViews().AddNewtonsoftJson();
        services.AddRazorPages();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedAccount = false;
        });

        services.ConfigureApplicationCookie(options =>
        {
            // Cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(1);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

        // Route Helping
        services.Configure<RouteSettings>(options => Configuration.GetSection("RouteSettings").Bind(options));
        services.AddSingleton<IAbsoluteUrlHelper, AbsoluteUrlHelper>();

        // Emails
        services.Configure<EmailSettings>(options => Configuration.GetSection("EmailSettings").Bind(options));
        services.AddTransient<IEmailSender, EmailSenderService>();
        services.AddTransient<IViewToStringRenderer, ViewToStringRenderer>();

        // Blob Storage
        services.Configure<StorageSettings>(options => options.ConnectionString = Configuration.GetConnectionString("BlobStorage"));
        services.AddTransient<IBlobStorageService, BlobStorageService>();

        // Strava
        services.Configure<StravaSettings>(options => Configuration.GetSection("Strava").Bind(options));
        services.AddTransient<IStravaService, StravaService>();

        // Page Model Builders
        services.AddTransient<IGravatarResolver, GravatarResolver>();
        services.AddTransient<IHomePageViewModelBuilder, HomePageViewModelBuilder>();
        services.AddTransient<ISkaterProgressViewModelBuilder, SkaterProgressViewModelBuilder>();
        services.AddTransient<ISkaterLogViewModelBuilder, SkaterLogViewModelBuilder>();
        services.AddTransient<IStravaImportViewModelBuilder, StravaImportViewModelBuilder>();
        services.AddTransient<IEventDetailsViewModelBuilder, EventDetailsViewModelBuilder>();
        services.AddTransient<IErrorViewModelBuilder, ErrorViewModelBuilder>();
        services.AddTransient<IEventStatisticsViewModelBuilder, EventStatisticsViewModelBuilder>();
        services.AddTransient<IStatisticLeadersViewModelBuilder, StatisticLeadersViewModelBuilder>();

        // Data
        services.AddSingleton<ICheckPointRepository, CheckPointRepository>();
        
        // Commands & Queries
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(StravaPendingImportsQuery));
        });

        // Misc
        services.AddTransient<ISkaterTargetAnalyser, SkaterTargetAnalyser>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/500.html");
        }

        app.UseStatusCodePagesWithRedirects("/Error/{0}");

        // Registered before static files to always set header
        app.UseHsts(hsts => hsts.MaxAge(365));
        app.UseXContentTypeOptions();
        app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
        app.UseXfo(opt => opt.SameOrigin());
        app.UseReferrerPolicy(opt => opt.NoReferrerWhenDowngrade());
        app.UseCsp(opt => opt.DefaultSources(s => s.Self())
                             .ScriptSources(s => s.Self().UnsafeInline().UnsafeEval().CustomSources("https://ajax.aspnetcdn.com", "https://cdn.jsdelivr.net", "https://unpkg.com/vue@2.6.12/dist/vue.js"))
                             .StyleSources(s => s.Self().UnsafeInline())
                             .ConnectSources(s => s.Self().CustomSources("https://rggskatemarathonfunctions.azurewebsites.net"))
                             .FrameSources(s => s.None())
                             .FrameAncestors(s => s.None())
                             .ImageSources(s => s.Self().CustomSources("data:", "https:", "https://www.gravatar.com")));

        app.UseHttpsRedirection();

        var staticFileTypeProvider = new FileExtensionContentTypeProvider();
        staticFileTypeProvider.Mappings[".webmanifest"] = "application/manifest+json";
        app.UseStaticFiles(new StaticFileOptions { 
            OnPrepareResponse = ctx =>
            {
                const int yearInSeconds = 365 * 24 * 60 * 60;
                ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={yearInSeconds}";
            },
            ContentTypeProvider = staticFileTypeProvider
        });

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}