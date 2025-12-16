using ClinicManagementAbp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ClinicManagementAbp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ClinicManagementAbpController : AbpControllerBase
{
    protected ClinicManagementAbpController()
    {
        LocalizationResource = typeof(ClinicManagementAbpResource);
    }
}
