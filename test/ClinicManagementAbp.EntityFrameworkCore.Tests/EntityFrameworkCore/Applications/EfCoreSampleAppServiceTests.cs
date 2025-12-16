using ClinicManagementAbp.Samples;
using Xunit;

namespace ClinicManagementAbp.EntityFrameworkCore.Applications;

[Collection(ClinicManagementAbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ClinicManagementAbpEntityFrameworkCoreTestModule>
{

}
