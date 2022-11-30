using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WeatherForecastApi.Filters;

namespace WeatherForecastApi.Extensions;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            // Allows API return versions in the response header (api-supported-versions).
            options.ReportApiVersions = true;
            // Allows to choose whether they would like to place the parameter in the URL or in the request header
            //config.ApiVersionReader = ApiVersionReader.Combine(
            //    new UrlSegmentApiVersionReader(),
            //    new HeaderApiVersionReader("x-api-version"),
            //    new MediaTypeApiVersionReader("x-api-version"));
        });

        // Allows to discover versions
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen(options =>
        {
            // TODO: Use this sentence if you use only one assembly with comments.
            // options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
            //    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);

            // TODO: Replace according to your namespace. ej: "ElGuerre.Versioning.xxx."
            //       Use this one if you've comments in more than one assembly.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.GetName().Name?.StartsWith("WeatherForecastApi") ?? false);

            foreach (var assembly in assemblies)
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory,
                    $"{assembly.GetName().Name}.xml");
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
            }

            options.OperationFilter<SwaggerDefaultValuesFilter>();
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }
}
