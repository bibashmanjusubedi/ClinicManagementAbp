using Volo.Abp.Modularity;

namespace ClinicManagementAbp;

public abstract class ClinicManagementAbpApplicationTestBase<TStartupModule> : ClinicManagementAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
