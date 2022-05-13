using AdminAPIServices.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminAPIServices
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(jwtOptions =>
               {
                   var key = Configuration.GetValue<string>("JwtConfig:Key");
                   var keyBytes = Encoding.ASCII.GetBytes(key);
                   jwtOptions.SaveToken = true;
                   jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                       ValidateLifetime = true,
                       ValidateAudience = false,
                       ValidateIssuer = false,
                       ClockSkew = TimeSpan.Zero
                   };
               });
            //services.AddSingleton(typeof(IJwtTokenManager), typeof(JwtTokenManager));
            services.AddScoped(typeof(IJwtTokenManager), typeof(JwtTokenManager));

            //services.AddSingleton<IAirline, AirlineRepository>();
            services.AddScoped<IFlight, FlightRepository>();

            var connectionString = Configuration.GetConnectionString("DBConnection");
            services.AddDbContext<InventoryContext>
                (options => options.UseSqlServer(connectionString));

            services.AddCors();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder
            //            .AllowAnyMethod()
            //            .AllowCredentials()
            //            .SetIsOriginAllowed((host) => true)
            //            .AllowAnyHeader());
            //});
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseCors("CorsPolicy");

            //app.UseCors(builder =>
            //{
            //    builder
            //    .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            //});

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials

            //app.UseCors(builder => builder.WithOrigins("http://localhost:4700").AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}
