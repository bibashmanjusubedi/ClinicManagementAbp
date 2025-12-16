using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ClinicManagementAbp.Data;
using Volo.Abp.DependencyInjection;

namespace ClinicManagementAbp.EntityFrameworkCore;

public class EntityFrameworkCoreClinicManagementAbpDbSchemaMigrator
    : IClinicManagementAbpDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreClinicManagementAbpDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ClinicManagementAbpDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ClinicManagementAbpDbContext>()
            .Database
            .MigrateAsync();
    }
}
