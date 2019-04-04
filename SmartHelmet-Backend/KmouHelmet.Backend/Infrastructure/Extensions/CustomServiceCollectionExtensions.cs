using System;
using KmouHelmet.Backend.Infrastructure.Filters;
using KmouHelmet.Backend.Mappers;
using KmouHelmet.Backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace KmouHelmet.Backend.Infrastructure.Extensions
{
    public static class CustomServiceCollectionExtensions
    {
        public static IServiceCollection AddAppInsight(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(configuration);

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<KmouDbContext>(
                options => options.UseMySql(configuration["ConnectionString"],
                mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(10, 2), ServerType.MariaDb);
                }
            ));

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            IHealthChecksBuilder hcBuilder = services.AddHealthChecks();

            hcBuilder
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddMySql(
                    configuration["ConnectionString"],
                    name: "KmouDB-check",
                    tags: new string[] { "kmoudb" });

            return services;
        }

        public static IServiceCollection AddCustomMVC(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSignalR();

            return services;
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "KmouHelmet - Monolithic API",
                    Version = "v1",
                    Description = "The Monolithic API. This is for KmouHelmet",
                    TermsOfService = "Terms Of Service"
                });
            });

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<DeviceMapperDtos>();
            services.AddTransient<GpsMapperDtos>();
            services.AddTransient<LocationMapperDtos>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IGpsRepository, GpsRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
