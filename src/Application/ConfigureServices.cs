using AutoMapper;
using FlightStatusService.Application.Common.Mappings;
using MediatR;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;


public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
