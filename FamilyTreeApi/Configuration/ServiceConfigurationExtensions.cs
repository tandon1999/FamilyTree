using FamilyTreeApi.Service.Implementation;
using FamilyTreeApi.Service.Interface;
using FamilyTreeApi.Shared;
using FamilyTreeApi.Shared.CurrentUser;
using FamilyTreeApi.Shared.DataBaseAccess.Dapper.Implementation;
using FamilyTreeApi.Shared.DataBaseAccess.Dapper.Interface;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Implementation;
using FamilyTreeApi.Shared.DataBaseAccess.GenericRepository.Interface;
using FamilyTreeApi.Shared.Encryptions;
using FamilyTreeApi.Shared.Service;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Shared.Services.Implementation;
using Shared.Services.Interface;
using System.Reflection.Metadata;

namespace FamilyTreeApi.Configuration
{
    public static class ServiceConfigurationExtensions
    {
        internal static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddService();
            services.AddSwaggerDocumentation();
            services.AddHttpContextAccessor();
            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddTransient<IDapperRepository, DapperRepository>();
            services.AddTransient<IFamilyMemberService,FamilyMemberService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IEncryptionService, EncryptionService>();





            return services;
        }



        internal static IServiceCollection AddService(this IServiceCollection services)
        {
            var managers = typeof(IService);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }

        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Family Tree API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement{
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                Array.Empty<string>()
            }
            });
            });
            return services;
        }

        /*internal static IServiceCollection AddGlobalVariable(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var utilityService = serviceProvider.GetService<IUtilityService>();
            List<StaticVariable> allStaticVariable = utilityService!.GetAllGlobalVariableAsync().Result.Data;

            GlobalVariable staticVariable = new();

            var optProp = staticVariable.GetType().GetProperties();
            foreach (var prop in optProp)
            {
                prop.SetValue(staticVariable, allStaticVariable.Where(d => d.Name == prop.Name).Select(d => d.Value).FirstOrDefault());
            }
            //services.Configure(staticVariable);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<GlobalVariable>>().Value);
            return services;
        }*/
    }
}
