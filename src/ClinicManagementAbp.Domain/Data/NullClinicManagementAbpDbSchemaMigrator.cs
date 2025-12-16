using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ClinicManagementAbp.Data;

/* This is used if database provider does't define
 * IClinicManagementAbpDbSchemaMigrator implementation.
 */
public class NullClinicManagementAbpDbSchemaMigrator : IClinicManagementAbpDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
