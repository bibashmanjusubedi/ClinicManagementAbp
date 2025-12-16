using Microsoft.Extensions.Localization;
using ClinicManagementAbp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ClinicManagementAbp;

[Dependency(ReplaceServices = true)]
public class ClinicManagementAbpBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ClinicManagementAbpResource> _localizer;

    public ClinicManagementAbpBrandingProvider(IStringLocalizer<ClinicManagementAbpResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
