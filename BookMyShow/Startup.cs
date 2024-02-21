using BookMyShow.Business;
using BookMyShow.Business.BusinessInterfaces;
using BookMyShow.Data;
using BookMyShow.Repository;
using BookMyShow.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BookMyShow
{
    [ExcludeFromCodeCoverage]

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //private async Task CreateUserRole(IServiceProvider serviceProvider)
        //{
        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    foreach (var roleName in new[] { "Admin", "Organizer", "Customer" })
        //    {
        //        if (!await roleManager.RoleExistsAsync(roleName))
        //        {
        //            await roleManager.CreateAsync(new IdentityRole { Name = roleName });
        //        }
        //    }
        //}
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IAuthenticationBusiness,AuthenticationBusiness>();
            services.AddScoped<IAdminBusiness,AdminBusiness>(); 
            services.AddScoped<IArtistBusiness,ArtistBusiness>();
            services.AddScoped<IBookingBusiness,BookingBusiness>();
            services.AddScoped<ICustomerBusiness,CustomerBusiness>();
            services.AddScoped<IEventBusiness,EventBusiness>();
            services.AddScoped<IOrganizerBusiness,OrganizerBusiness>();
            services.AddScoped<IVenueBusiness, VenueBusiness>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IBookingRepository,BookingRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVenueRepository, VenueRespository>();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BookMyShow",
                    Description = "Bookings made easy",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });
            app.UseRouting();   
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
            });

            //CreateUserRole(serviceProvider).Wait();
        }
    }
}
