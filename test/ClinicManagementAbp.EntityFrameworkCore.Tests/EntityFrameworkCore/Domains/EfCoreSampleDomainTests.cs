using ClinicManagementAbp.Samples;
using Xunit;

namespace ClinicManagementAbp.EntityFrameworkCore.Domains;

[Collection(ClinicManagementAbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ClinicManagementAbpEntityFrameworkCoreTestModule>
{

}
