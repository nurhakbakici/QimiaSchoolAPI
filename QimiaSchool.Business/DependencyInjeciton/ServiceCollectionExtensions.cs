using Microsoft.Extensions.DependencyInjection;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations;
using System.Reflection;

namespace QimiaSchool.Business.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection serviceCollection)
    {
        
        AddManagers(serviceCollection);
        AddMediatRHandlers(serviceCollection);
        AddAutoMapper(serviceCollection);

        return serviceCollection;
    }

    private static void AddMediatRHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static void AddManagers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICacheService, CacheService>();
        serviceCollection.AddScoped<IStudentManager, StudentManager>();
        serviceCollection.AddScoped<IEnrollmentManager, EnrollmentManager>();
        serviceCollection.AddScoped<ICourseManager, CourseManager>();
    }

    private static void AddAutoMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
