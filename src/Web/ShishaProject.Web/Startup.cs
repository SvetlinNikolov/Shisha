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
    using Stripe;
    using ShishaProject.Services.Strategy;
    using ShishaProject.Services.Data.Interfaces;

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
                    opt.SupportedCultures = GlobalConstants.AvailableLanguages.Select(x => new CultureInfo(x)).ToList();
                    opt.SupportedUICultures = GlobalConstants.AvailableLanguages.Select(x => new CultureInfo(x)).ToList();
                    opt.RequestCultureProviders = new List<IRequestCultureProvider>
                    {
                        new CookieRequestCultureProvider(),
                        new QueryStringRequestCultureProvider(),
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
                  options.LoginPath = "/{culture?}/Users/LoginUser";
                  options.LogoutPath = "/{culture?}/Users/LogoutUser";
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

            // Logging
            services.AddSingleton<IShishaLogger, ShishaLogger>();

            // Localization
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Config Registration
            services.AddOptions();
            services.Configure<ProductsEndpointsConfig>(this.configuration.GetSection("Endpoints"));
            services.Configure<UsersEndpointsConfig>(this.configuration.GetSection("Endpoints"));
            services.Configure<CartEndpointsConfig>(this.configuration.GetSection("Endpoints"));
            services.Configure<StripeConfig>(this.configuration.GetSection("Stripe"));
            services.Configure<JwtConfig>(this.configuration.GetSection("Jwt"));

            // Payment
            services.AddTransient<IPaymentStrategy, PaymentStrategy>();
            services.AddTransient<IPaymentService, StripeService>();

            services.AddTransient<ICartService, CartService>();

            // Http
            services.AddTransient<IRestClient, RestClientService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient();

            // Products
            services.AddTransient<IProductsService, ProductsService>();

            // Users
            services.AddTransient<IUsersService, UsersService>();

            // Email
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<IEmailService, EmailService>();

            // Security
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IUserSecurityService, UserSecurityService>();
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

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            app.ConfigureExceptionHandler(shishaLogger);

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
