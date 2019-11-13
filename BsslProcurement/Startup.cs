using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace BsslProcurement
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment  env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IHostingEnvironment Env { get; set; }
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

            services.AddDbContext<ProcurementDBContext> (options => options.UseSqlServer(connection, b => b.MigrationsAssembly("BsslProcurement")));

            services.AddDbContext<BSSLSYS_ITF_DEMOContext>(options => options.UseSqlServer(conn, b => b.MigrationsAssembly("BsslProcurement")));


            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<ProcurementDBContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

                options.LoginPath = "/Identity/Account/Login";
                options.SlidingExpiration = true;
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


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Procurement API Docs", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
           
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
