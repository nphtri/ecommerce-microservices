using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRODUCTSERVICE.API.AWS;
using PRODUCTSERVICE.API.Data;
using PRODUCTSERVICE.API.Services;

namespace PRODUCTSERVICE.API
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
      // Cors configure
      services.AddCors(opts =>
      {
        opts.AddPolicy("AllowAll", builder =>
              {
                builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                //.AllowCredentials();
              });
      });
      services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:PRODUCTSERVICE.APIDb"]));
      services.AddControllers();
      services.AddRouting(opts => opts.LowercaseUrls = true);
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddScoped<IArtService, ArtService>();
      services.AddScoped<IArtistService, ArtistService>();
      services.AddScoped<IBannerService, BannerService>();

      services.AddAWSService<IAmazonS3>();
      services.AddScoped<IAWSService, AWSService>();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      // Add jwt authentication
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
          .AddJwtBearer(cfg =>
          {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"])),
              ValidateIssuer = true,
              ValidIssuer = Configuration["Jwt:Iss"],
              ValidateAudience = false,
              RequireExpirationTime = false
            };
          });
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "PRODUCTSERVICE.API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
      });
    }

    private void WriteProfile(string profileName, string keyId, string secret, RegionEndpoint region)
    {
      Console.WriteLine($"Create the [{profileName}] profile...");
      var options = new CredentialProfileOptions
      {
        AccessKey = keyId,
        SecretKey = secret
      };
      var profile = new CredentialProfile(profileName, options);
      profile.Region = RegionEndpoint.APSoutheast1;
      var sharedFile = new SharedCredentialsFile();
      sharedFile.RegisterProfile(profile);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PRODUCTSERVICE.API v1"));
      }

      app.UseCors("AllowAll");

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
      PrepDb.PrepPopulation(app);
    }
  }
}
