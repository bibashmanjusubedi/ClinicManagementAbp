using Volo.Abp.Modularity;

namespace ClinicManagementAbp;

/* Inherit from this class for your domain layer tests. */
public abstract class ClinicManagementAbpDomainTestBase<TStartupModule> : ClinicManagementAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
