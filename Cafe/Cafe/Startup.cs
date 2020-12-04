using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cafe
{
    public class Startup
    {

        public IConfiguration Config { get; }

        public Startup(IConfiguration config)
        {
            Config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var aa = Config.GetConnectionString("BloggingDatabase");
            MicroORM.ORMConfig.ConnectionString = aa;
            MicroORM.ORMConfig.DbType = MicroORM.DbType.MSSQL;

            //services.AddScoped<CookieEvents>();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option =>
            {
                option.Cookie.Name = "Cafe";
                option.LoginPath = "/Home/Login";
                option.LogoutPath = "/Home/Index";
                //option.EventsType = typeof(CookieEvents);
            });

            

            services.AddAuthorization(config =>
            {
                config.AddPolicy("AdminPages", c =>
                {
                    c.RequireClaim(ClaimTypes.Role, "Admin");
                    
                });
                
            });

            services.AddMvc().AddRazorPagesOptions(c=> {
                c.Conventions.AuthorizeFolder("/Admin", "AdminPages");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                
            }
            app.UseStatusCodePagesWithRedirects("/Error/{0}");


            var cultureInfo = new CultureInfo("en");


            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseStaticFiles();
            app.UseAuthentication();
            
            app.UseMvcWithDefaultRoute();

        }
    }
}
