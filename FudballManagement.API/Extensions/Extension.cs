using FudballManagement.Application.Mappers;
using FudballManagement.Application.Services.Implamentations;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Infrastructure.DbContexts;
using FudballManagement.Infrastructure.Repositories.Implamentation;
using FudballManagement.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FudballManagement.API.Extensions;
public static  class Extension
{
    public static void AddDbConnection(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(option => option.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
    }

    public static void AddDependService(this IServiceCollection services)
    {
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IStadiumService, StadiumService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IOrderService, OrderService>();

    }

    public static void AddConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "StartupProject", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
       {
           {
               new OpenApiSecurityScheme
               {
                   Reference = new OpenApiReference
                   {
                       Type = ReferenceType.SecurityScheme,
                       Id = "Bearer"
                   }
               },
               new string[]{}
           }
       });
        });
    }

    public static void AddJWTConfugration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    RequireExpirationTime = false
                };
            });
    }

    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
        });
        return services;
    }

}
