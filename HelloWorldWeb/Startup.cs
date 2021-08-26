using System;
using System.IO;
using System.Reflection;
using HelloWorldWeb;
using HelloWorldWeb.Controllers;
using HelloWorldWeb.Data;
using HelloWorldWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HelloWorldWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static string ConvertHerokuStringToASPString(string herokuConnectionString)
        {
            var databaseUri = new Uri(herokuConnectionString);
            string[] userInfo = databaseUri.UserInfo.Split(':');

            int port = databaseUri.Port;
            string host = databaseUri.Host;
            string userId = userInfo[0];
            string password = userInfo[1];
            string database = databaseUri.AbsolutePath[1..];

            string result = $"Host={host};Port={port};Database={database};User Id={userId};Password={password};Pooling=true;SSL Mode=Require;TrustServerCertificate=True;Include Error Detail=True";
            return result;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        ///
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddScoped<ITeamService, DbTeamService>();
            services.AddSingleton<ITimeService, TimeService>();
            services.AddSingleton<IBroadcastService, BroadcastService>();
            services.AddSingleton<IWeatherControllerSettings, WeatherControllerSettings>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello World API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
            string databaseURL = Environment.GetEnvironmentVariable("DATABASE_URL");
            databaseURL = databaseURL != null ? ConvertHerokuStringToASPString(databaseURL) : this.Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseURL));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();

            this.AssignRoleProgramatically(services.BuildServiceProvider());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<MessageHub>("/messagehub");
                endpoints.MapRazorPages();
            });
        }

        private async void AssignRoleProgramatically(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var user = await userManager.FindByNameAsync("dragos.duma@gmail.com");
            await userManager.AddToRoleAsync(user, "Administrators");
        }
    }
}