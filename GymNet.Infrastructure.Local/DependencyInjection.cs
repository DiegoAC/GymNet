using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GymNet.Application.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GymNet.Infrastructure.Local;

public sealed class SystemClock : IDateTimeProvider { public DateTime UtcNow => DateTime.UtcNow; }
public sealed class DevCurrentUser : ICurrentUser { public string UserId => "dev-user"; }

public static class DependencyInjection
{
    public static IServiceCollection AddLocalInfra(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemClock>();
        services.AddSingleton<ICurrentUser, DevCurrentUser>();
        return services;
    }
}

