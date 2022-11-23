using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Filters;

namespace WeatherForecastApi.Extensions;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
            // allows API return versions in the response header (api-supported-versions).
            o.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen(o =>
        {
            //o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
            //    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
            o.OperationFilter<SwaggerDefaultValuesFilter>();
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }
}
