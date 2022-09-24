using System.Reflection;
using BaseCleanArchitecture.DependencyInjection.OpenApi.ApiGroup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace BaseCleanArchitecture.DependencyInjection.OpenApi;

public static class OpenApiFactory
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services, bool addJwtBearer = true, Action<SwaggerGenOptions>? options = null)
    {
        if (options is not null)
        {
            services.Configure(options);
        }

        services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(s => s.FullName!.Replace("+", "."));
            o.EnableAnnotations(true, true);
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


            if (addJwtBearer)
            {
                o.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                var securitySchema = new OpenApiSecurityScheme()
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

                o.AddSecurityDefinition("Bearer", securitySchema);

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });
            }
        });

        return services;
    }

    public static IServiceCollection AddApiGroupNames(this IServiceCollection services)
        => services.AddApiGroupNames<ApiGroupNames>();

    public static IServiceCollection AddApiGroupNames<TApiGroupNames>(this IServiceCollection services)
        where TApiGroupNames : Enum
    {
        services.Configure<SwaggerGenOptions>(o =>
        {
            typeof(TApiGroupNames).GetFields().Skip(1).ToList().ForEach(f =>
            {
                var info = f.GetCustomAttributes(typeof(GroupInfoAttribute), false)
                    .OfType<GroupInfoAttribute>()
                    .FirstOrDefault(new GroupInfoAttribute(string.Empty, string.Empty, string.Empty));
                o.SwaggerDoc(f.Name, new OpenApiInfo
                {
                    Title = info.Title,
                    Version = info.Version,
                    Description = info.Description,
                });
            });

            o.DocInclusionPredicate((docName, apiDescription) =>
            {
                if (!apiDescription.TryGetMethodInfo(out _)) return false;
                if (string.Equals(docName, "All", StringComparison.OrdinalIgnoreCase)) return true;
                var nationalist = apiDescription.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is ApiGroupAttribute);
                if (string.Equals(docName, "NoGroup", StringComparison.OrdinalIgnoreCase)) return nationalist == null;
                if (nationalist != null)
                {
                    var actionFilter = (ApiGroupAttribute)nationalist;
                    return actionFilter.GroupNames.Any(x => string.Equals(x.ToString().Trim(), docName, StringComparison.OrdinalIgnoreCase));
                }

                return false;
            });
        });
        return services;
    }


    public static IApplicationBuilder UseOpenApi<TApiGroupNames>(this IApplicationBuilder app, bool useApiGroupName = true)
        where TApiGroupNames : Enum
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            if (useApiGroupName)
            {
                typeof(TApiGroupNames).GetFields().Skip(1).ToList().ForEach(f =>
                {
                    var info = f.GetCustomAttributes(typeof(GroupInfoAttribute), false)
                        .OfType<GroupInfoAttribute>()
                        .FirstOrDefault(new GroupInfoAttribute(string.Empty, string.Empty, string.Empty));
                    c.SwaggerEndpoint($"/swagger/{f.Name}/swagger.json", info.Title);
                });
            }
            else
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", string.Empty);
            }

            c.DocExpansion(DocExpansion.None);
        });
        return app;
    }

    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, bool useApiGroupName = true)
        => app.UseOpenApi<ApiGroupNames>(useApiGroupName);
}