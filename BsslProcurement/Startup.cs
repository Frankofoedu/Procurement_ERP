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
using DcProcurement.Users;
using BsslProcurement.Filters;

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
            //if (Env.IsDevelopment())
            //{
                connection = Configuration.GetValue<string>("LocalProcurementTestConnection");
                conn = Configuration.GetValue<string>("LocalITFTestConnection");
            //}
            //else
            //{
            //connection = Configuration.GetValue<string>("OnlineProcurementTestConnection");

            //TODO: update to server connection
            //conn = Configuration.GetValue<string>("OnlineITFTestConnection");
            // }

            services.AddDbContext<ProcurementDBContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("BsslProcurement")));

            services.AddDbContext<BSSLSYS_ITF_DEMOContext>(options => options.UseSqlServer(conn, b => b.MigrationsAssembly("BsslProcurement")));

            services.AddSingleton<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IStaffLayoutViewModelService, StaffLayoutViewModelService>();
            services.AddScoped<IWorkFlowService, WorkFlowService>();
            services.AddScoped<IItemGridViewModelService, ItemGridViewModelService>();
            services.AddScoped<IRequisitionService, RequisitionService>();
            services.AddScoped<IProcurementService, ProcurementService>();
            services.AddScoped<IGroupManagement, GroupManagementService>();
            services.AddSingleton<IRazorPagesControllerDiscovery, RazorPagesControllerDiscovery>();

            services.AddIdentity<User, UserRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
               
            }).AddDefaultUI().AddEntityFrameworkStores<ProcurementDBContext>();

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

            services.AddRazorPages().AddMvcOptions(options => options.Filters.Add(typeof(DynamicAuthorizationFilter))).AddRazorRuntimeCompilation();
            services.AddControllers();
            services.AddAuthentication().AddCookie("Vendors", o =>
            {// Cookie settings
                o.Cookie.HttpOnly = true;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(15);

                o.LoginPath = "/VendorIdentity/Account/Login";
                o.SlidingExpiration = true;
                o.Cookie.IsEssential = true;
                o.ForwardAuthenticate = "Identity.Application";
            });
            services.AddAuthorization();

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1";
                option.InstanceName = "master";
            });

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
            app.UseSession();
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
