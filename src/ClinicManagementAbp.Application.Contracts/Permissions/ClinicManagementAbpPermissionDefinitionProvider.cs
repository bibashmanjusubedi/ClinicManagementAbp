using ClinicManagementAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Localization;
using Volo.Abp.Authorization.Permissions;
using ClinicManagementAbp.Permissions;

namespace ClinicManagementAbp.Permissions;

public class ClinicManagementAbpPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ClinicManagementAbpPermissions.MyPermission1, L("Permission:MyPermission1"));
        var clinicGroup = context.AddGroup(ClinicManagementAbpPermissions.GroupName, L("ClinicManagement"));

        // Patients (Receptionist + Admin)
        var patientsPermission = clinicGroup.AddPermission(ClinicManagementAbpPermissions.Patients.Create, L("Patients.Create"));
        patientsPermission.AddChild(ClinicManagementAbpPermissions.Patients.Edit, L("Patients.Edit"));
        patientsPermission.AddChild(ClinicManagementAbpPermissions.Patients.SoftDelete, L("Patients.SoftDelete"));
        patientsPermission.AddChild(ClinicManagementAbpPermissions.Patients.ViewAll, L("Patients.ViewAll"));
        patientsPermission.AddChild(ClinicManagementAbpPermissions.Patients.ViewOne, L("Patients.ViewOne"));


        // Doctors (Admin only)
        var doctorsPermission = clinicGroup.AddPermission(ClinicManagementAbpPermissions.Doctors.Create, L("Doctors.Create"));
        doctorsPermission.AddChild(ClinicManagementAbpPermissions.Doctors.Edit, L("Doctors.Edit"));
        doctorsPermission.AddChild(ClinicManagementAbpPermissions.Doctors.Delete, L("Doctors.Delete"));
        doctorsPermission.AddChild(ClinicManagementAbpPermissions.Doctors.ViewAll, L("Doctors.ViewAll"));
        doctorsPermission.AddChild(ClinicManagementAbpPermissions.Doctors.ViewOne, L("Doctors.ViewOne"));

        // Appointments
        var appointmentsPermission = clinicGroup.AddPermission(ClinicManagementAbpPermissions.Appointments.Create, L("Appointments.Create"));
        appointmentsPermission.AddChild(ClinicManagementAbpPermissions.Appointments.Reschedule, L("Appointments.Reschedule"));
        appointmentsPermission.AddChild(ClinicManagementAbpPermissions.Appointments.Cancel, L("Appointments.Cancel"));
        appointmentsPermission.AddChild(ClinicManagementAbpPermissions.Appointments.Complete, L("Appointments.Complete"));
        appointmentsPermission.AddChild(ClinicManagementAbpPermissions.Appointments.Delete, L("Appointments.Delete"));
        appointmentsPermission.AddChild(ClinicManagementAbpPermissions.Appointments.ViewAll, L("Appointments.ViewAll"));
        appointmentsPermission.AddChild(ClinicManagementAbpPermissions.Appointments.ViewOne, L("Appointments.ViewOne"));
        appointmentsPermission.AddChild(ClinicManagementAbpPermissions.Appointments.ViewOwn, L("Appointments.ViewOwn"));

        // Doctor Schedules
        var schedulesPermission = clinicGroup.AddPermission(ClinicManagementAbpPermissions.DoctorSchedules.Create, L("DoctorSchedules.Create"));
        schedulesPermission.AddChild(ClinicManagementAbpPermissions.DoctorSchedules.Edit, L("DoctorSchedules.Edit"));
        schedulesPermission.AddChild(ClinicManagementAbpPermissions.DoctorSchedules.Delete, L("DoctorSchedules.Delete"));
        schedulesPermission.AddChild(ClinicManagementAbpPermissions.DoctorSchedules.ViewAll, L("DoctorSchedules.ViewAll"));
        schedulesPermission.AddChild(ClinicManagementAbpPermissions.DoctorSchedules.ViewOne, L("DoctorSchedules.ViewOne"));
        schedulesPermission.AddChild(ClinicManagementAbpPermissions.DoctorSchedules.ViewOwn, L("DoctorSchedules.ViewOwn"));


        var identityGroup = context.AddGroup("Identity", L("IdentityManagement"));
        identityGroup.AddPermission(ClinicManagementAbpPermissions.Identity.ManageRoles, L("ManageRoles"));
        identityGroup.AddPermission(ClinicManagementAbpPermissions.Identity.CreateUsers, L("CreateUsers"));


    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ClinicManagementAbpResource>(name);
    }
}
