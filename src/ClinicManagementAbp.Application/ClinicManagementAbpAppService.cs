using ClinicManagementAbp.Localization;
using Volo.Abp.Application.Services;

namespace ClinicManagementAbp;

/* Inherit your application services from this class.
 */
public abstract class ClinicManagementAbpAppService : ApplicationService
{
    protected ClinicManagementAbpAppService()
    {
        LocalizationResource = typeof(ClinicManagementAbpResource);
    }
}
