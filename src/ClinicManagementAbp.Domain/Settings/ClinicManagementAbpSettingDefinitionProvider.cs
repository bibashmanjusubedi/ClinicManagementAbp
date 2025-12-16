using Volo.Abp.Settings;

namespace ClinicManagementAbp.Settings;

public class ClinicManagementAbpSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ClinicManagementAbpSettings.MySetting1));
    }
}
