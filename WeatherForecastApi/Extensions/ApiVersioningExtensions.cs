using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WeatherForecastApi.Filters;

namespace WeatherForecastApi.Extensions;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.DefaultApiVersion = new ApiVersion(1, 0);
            // Allows API return versions in the response header (api-supported-versions).
            config.ReportApiVersions = true;
            // Allows to choose whether they would like to place the parameter in the URL or in the request header
            //config.ApiVersionReader = ApiVersionReader.Combine(
            //    new UrlSegmentApiVersionReader(),
            //    new HeaderApiVersionReader("x-api-version"),
            //    new MediaTypeApiVersionReader("x-api-version"));
        });

        // Allows to discover versions
        services.AddVersionedApiExplorer(config =>
        {
            config.GroupNameFormat = "'v'VVV";
            config.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen(config =>
        {
            config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
            config.OperationFilter<SwaggerDefaultValuesFilter>();
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }
}
