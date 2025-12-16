using ClinicManagementAbp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ClinicManagementAbp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ClinicManagementAbpEntityFrameworkCoreModule),
    typeof(ClinicManagementAbpApplicationContractsModule)
)]
public class ClinicManagementAbpDbMigratorModule : AbpModule
{
}
