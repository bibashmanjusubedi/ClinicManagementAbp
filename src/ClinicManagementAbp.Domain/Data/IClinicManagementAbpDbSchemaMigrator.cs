using System.Threading.Tasks;

namespace ClinicManagementAbp.Data;

public interface IClinicManagementAbpDbSchemaMigrator
{
    Task MigrateAsync();
}
