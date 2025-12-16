using Volo.Abp.Modularity;

namespace ClinicManagementAbp;

[DependsOn(
    typeof(ClinicManagementAbpApplicationModule),
    typeof(ClinicManagementAbpDomainTestModule)
)]
public class ClinicManagementAbpApplicationTestModule : AbpModule
{

}
