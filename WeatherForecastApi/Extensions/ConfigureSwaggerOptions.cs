using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WeatherForecastApi.Extensions;

// Allows multi-version 
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        // TODO: Add authenticacion stuffs
        //options.AddSecurityDefinition(...);
        //options.AddSecurityRequirement(...);

        foreach (var description in this.apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateOpenApiInfo(description));
            options.OrderActionsBy((apiDesc) => $"{apiDesc.RelativePath}");
            options.DescribeAllParametersInCamelCase();
            options.CustomSchemaIds(this.DefaultSchemaIdSelector);
        }
    }

    private string DefaultSchemaIdSelector(Type modelType)
    {
        if (!modelType.IsConstructedGenericType) return modelType.Name;

        var prefix = modelType.GetGenericArguments()
            .Select(this.DefaultSchemaIdSelector)
            .Aggregate((previous, current) => previous + current);

        return modelType.Name.Split('`').First() + "Of" + prefix;
    }

    private static OpenApiInfo CreateOpenApiInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "Weather Forecast API",
            Version = description.ApiVersion.ToString(),
        };

        if (description.IsDeprecated)
        {
            info.Description += " (deprecated)";
        }

        return info;
    }
}
