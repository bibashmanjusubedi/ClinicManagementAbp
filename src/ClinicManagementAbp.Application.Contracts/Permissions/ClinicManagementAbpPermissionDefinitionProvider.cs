using ClinicManagementAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ClinicManagementAbp.Permissions;

public class ClinicManagementAbpPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ClinicManagementAbpPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(ClinicManagementAbpPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ClinicManagementAbpResource>(name);
    }
}
