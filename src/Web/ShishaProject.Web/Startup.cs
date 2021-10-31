namespace ShishaProject.Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using ExampleAPIClient.Client;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using ShishaProject.Common;
    using ShishaProject.Data;
    using ShishaProject.Data.Common;
    using ShishaProject.Data.Common.Repositories;
    using ShishaProject.Data.Models;
    using ShishaProject.Data.Repositories;
    using ShishaProject.Services;
    using ShishaProject.Services.Data;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Services.Mapping;
    using ShishaProject.Services.Messaging;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup()
        {
            this.configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile("productsEndpoints.json", optional: false, reloadOnChange: false)
          .AddJsonFile("usersEndpoints.json", optional: false, reloadOnChange: false)
          .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<RequestLocalizationOptions>(
                opt =>
                {
                    opt.DefaultRequestCulture = new RequestCulture(GlobalConstants.MainLanguage);
                    opt.SupportedCultures = GlobalConstants.AvailableLanguages.Select(x => new CultureInfo(x)).ToList();
                    opt.SupportedUICultures = GlobalConstants.AvailableLanguages.Select(x => new CultureInfo(x)).ToList();
                    opt.RequestCultureProviders = new List<IRequestCultureProvider>
                    {
                        new QueryStringRequestCultureProvider(),
                        new CookieRequestCultureProvider(),
                    };
                });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(
              CookieAuthenticationDefaults.AuthenticationScheme, (options) =>
              {
                  options.LoginPath = "/Users/LoginUser";
                  options.LogoutPath = "/Users/LogoutUser";
              });

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => false;
                        //options.MinimumSameSitePolicy = SameSiteMode.None;
                        // this has to be none in production!!!!
                        options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                    });


            services.AddControllersWithViews(
                options =>
                    {
                        //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation()
                      .AddNewtonsoftJson();

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Localization
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();


            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Config Registration
            services.AddOptions();
            services.Configure<ProductsEndpointsConfig>(this.configuration.GetSection("Endpoints"));
            services.Configure<UsersEndpointsConfig>(this.configuration.GetSection("Endpoints"));

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();

            // Http
            services.AddTransient<IRestClient, RestClientService>();

            // Products
            services.AddTransient<IProductsService, ProductsService>();

            // Users
            services.AddTransient<IUsersService, UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select((item) => Assembly.Load(item)).ToArray());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{language}/{controller=Home}/{action=Index}/{id?}", new { language = GlobalConstants.MainLanguage });
                        endpoints.MapRazorPages();
                    });
        }
    }
}
