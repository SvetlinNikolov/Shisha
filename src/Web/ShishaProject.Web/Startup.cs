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
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using ShishaProject.Common;
    using ShishaProject.Services;
    using ShishaProject.Services.Data.Models.Configs;
    using ShishaProject.Services.Interfaces;
    using ShishaProject.Services.Mapping;
    using ShishaProject.Services.Messaging;
    using ShishaProject.Common.ExceptionHandling;
    using ShishaProject.Web.Middlewares;
    using ShishaProject.Common.Helpers;
    using ShishaProject.Web.ViewModels.Payment;
    using Stripe;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup()
        {
            this.configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile("productsEndpoints.json", optional: false, reloadOnChange: true)
          .AddJsonFile("usersEndpoints.json", optional: false, reloadOnChange: true)
          .AddJsonFile("cartEndpoints.json", optional: false, reloadOnChange: true)
          .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            StripeConfiguration.ApiKey = this.configuration.GetSection("Stripe")["SecretKey"];

            services.Configure<RequestLocalizationOptions>(
                opt =>
                {
                    opt.DefaultRequestCulture = new RequestCulture(GlobalConstants.MainLanguage);
                    // add multi culture support
                    //opt.SupportedCultures = GlobalConstants.AvailableLanguages.Select(x => new CultureInfo(x)).ToList();
                    //opt.SupportedUICultures = GlobalConstants.AvailableLanguages.Select(x => new CultureInfo(x)).ToList();
                    opt.SupportedCultures = new List<CultureInfo> { new CultureInfo(GlobalConstants.MainLanguage) };
                    opt.SupportedUICultures = new List<CultureInfo> { new CultureInfo(GlobalConstants.MainLanguage) };
                    opt.RequestCultureProviders = new List<IRequestCultureProvider>
                    {
                        new QueryStringRequestCultureProvider(),
                        new CookieRequestCultureProvider(),
                        new AcceptLanguageHeaderRequestCultureProvider(),
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(this.configuration);

            //Logging
            services.AddSingleton<IShishaLogger, ShishaLogger>();

            // Localization
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Config Registration
            services.AddOptions();
            services.Configure<ProductsEndpointsConfig>(this.configuration.GetSection("Endpoints"));
            services.Configure<UsersEndpointsConfig>(this.configuration.GetSection("Endpoints"));
            services.Configure<StripeConfig>(this.configuration.GetSection("Stripe"));

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();

            // Payment
            services.AddTransient<IStripeService, StripeService>();

            // Http
            services.AddTransient<IRestClient, RestClientService>();

            // Products
            services.AddTransient<IProductsService, ProductsService>();

            // Users
            services.AddTransient<IUsersService, UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IShishaLogger shishaLogger)
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

            app.ConfigureExceptionHandler(shishaLogger);
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
