using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.Services;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BsslProcurement
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IWebHostEnvironment Env { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            string connection;
            string conn;
            if (Env.IsDevelopment())
            {
                connection = @"Server=.\SQLEXPRESS;Database=DcProcurement;Trusted_Connection=True;ConnectRetryCount=0";
                conn = @"Server =.\SQLEXPRESS; Database=BSSLSYS_ITF;Trusted_Connection=True;";
            }
            else
            {
                connection = @"Data Source=WIN2016\BSSLDATAENGIN;Initial Catalog=DcProcurement;User ID=sa;Password=Bssl2019**;Integrated Security=False;
                  Trusted_Connection=True;ConnectRetryCount=0;MultipleActiveResultSets=true";

                //TODO:update to server connection
                conn = @"Data Source=WIN2016\BSSLDATAENGIN;Initial Catalog=BSSLSYS_ITFDEMO;User ID=sa;Password=Bssl2019**;Integrated Security=False;
                  Trusted_Connection=True;ConnectRetryCount=0;MultipleActiveResultSets=true";
            }

            services.AddDbContext<ProcurementDBContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("BsslProcurement")));

            services.AddDbContext<BSSLSYS_ITF_DEMOContext>(options => options.UseSqlServer(conn, b => b.MigrationsAssembly("BsslProcurement")));

            services.AddSingleton<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IStaffLayoutViewModelService, StaffLayoutViewModelService>();
            services.AddTransient<IWorkFlowService, WorkFlowService>();
            services.AddTransient<IItemGridViewModelService, ItemGridViewModelService>();
            services.AddTransient<IRequisitionService, RequisitionService>();

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<ProcurementDBContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(o =>
            {
                //o.LoginPath = new PathString("/Identity/Account/Login");
                // Cookie settings
                o.Cookie.HttpOnly = true;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(15);

                o.LoginPath = "/Identity/Account/Login";
                o.SlidingExpiration = true;
            });
            



            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));

            services.Configure<IdentityOptions>(options =>
            {

                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 7;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddControllers();
            services.AddAuthentication()
            .AddCookie("Vendors", o =>
            {// Cookie settings
                o.Cookie.HttpOnly = true;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(15);

                o.LoginPath = "/VendorIdentity/Account/Login";
                o.SlidingExpiration = true;
                o.Cookie.IsEssential = true;
                o.ForwardAuthenticate = "Identity.Application";
            });
            services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            app.UseDeveloperExceptionPage();
            //app.UseExceptionHandler("/Error");
            app.UseHsts();
            //}


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }


    }
}
