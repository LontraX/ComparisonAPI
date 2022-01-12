using Comparly.Core.Security;
using Comparly.Data;
using Comparly.Data.Models;
using Comparly.Data.Profiles;
using Comparly.Data.Services.AzureBlobStorageService.Implementation;
using Comparly.Data.Services.AzureBlobStorageService.Interface;
using Comparly.Data.Services.CloudinaryService.Implementation;
using Comparly.Data.Services.CloudinaryService.Interface;
using Comparly.Data.Services.Implementation;
using Comparly.Data.Services.Interface;
using Comparly.Data.Services.RapidApiService.Implementation;
using Comparly.Data.Services.RapidApiService.Interface;
using Comparly.Data.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly
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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer("Data Source=tcp:comparlycoredbserver.database.windows.net,1433;Initial Catalog=Comparly.Core_db;User Id=lontra@comparlycoredbserver;Password=Drolumide100%"));

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration.GetSection("AzureStorage:connectionString").Value);
            });
            services.AddSingleton<IStorageService,StorageService>();
            services.AddScoped<ICompareService,CompareService>();
            services.AddSingleton<IRapidApiService,RapidApiService>();
            services.AddSingleton<ICloudinaryService, CloudinaryService>();
            services.Configure<CloudinaryConfig>(Configuration.GetSection("CloudinaryConfig"));
            services.AddControllers();

            services.Configure<TokenConfig>(Configuration.GetSection("TokenConfig"));
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt => {
                var key = Encoding.ASCII.GetBytes(Configuration["TokenConfig:Secret"]);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false,
                    
                };
            });

            services.AddScoped<IJWT_TokenGenerator, JWT_TokenGenerator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Comparly", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };
                c.AddSecurityRequirement(securityRequirement);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment() || env.IsProduction())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Comparly v1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Comparly v1"));
            });
        }
    }
}
