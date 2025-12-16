using Volo.Abp.Modularity;

namespace ClinicManagementAbp;

[DependsOn(
    typeof(ClinicManagementAbpDomainModule),
    typeof(ClinicManagementAbpTestBaseModule)
)]
public class ClinicManagementAbpDomainTestModule : AbpModule
{

}
