using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using WebApi.Services;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using WebApi.Models;
using WebApi.Mappers;
using WebApi.Repositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApi
{
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
            //services.AddCors();
            services.AddDbContext<TaxiAPIContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("TaxiAPIContext")));
            services.AddControllers();
            
            services.AddSingleton(AutoMapperConfig.Initialize());

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }); 

                // configure DI for application services
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IOrderRepository, OrderRepository>();
                services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IDriverOrderService, DriverOrderService>();
            services.AddScoped<IUserService, UserService>();
                services.AddScoped<IOrderService, OrderService>();
                services.AddScoped<IDriverService, DriverService>();
                services.AddScoped<ITokenService, TokenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            // global cors policy
            app.UseCors(x => x.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
        
    }
}
