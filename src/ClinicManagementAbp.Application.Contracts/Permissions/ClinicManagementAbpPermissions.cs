namespace ClinicManagementAbp.Permissions;

public static class ClinicManagementAbpPermissions
{
    public const string GroupName = "ClinicManagementAbp";



    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Patients
    {
        public const string Create = GroupName + ".Patients.Create";
        public const string Edit = GroupName + ".Patients.Edit";
        public const string SoftDelete = GroupName + ".Patients.SoftDelete"; // Soft Delete
        public const string ViewAll = GroupName + ".Patients.ViewAll"; // View List of Patients
        public const string ViewOne = GroupName + ".Patients.ViewOne"; // View Particular Patient Details
    }

    public static class Doctors
    {
        public const string Create = GroupName + ".Doctors.Create";
        public const string Edit = GroupName + ".Doctors.Edit"; // Edit or Update Doctor Details
        public const string Delete = GroupName + ".Doctors.Delete";
        public const string ViewAll = GroupName + ".Doctors.ViewAll";
        public const string ViewOne = GroupName + ".Doctors.ViewOne";
    }



    public static class Appointments
    {
        public const string Create = GroupName + ".Appointments.Create";
        public const string Reschedule = GroupName + ".Appointments.Reschedule"; // Reschedule Appointment
        public const string Cancel = GroupName + ".Appointments.Cancel"; // Cancel Appointment
        public const string Delete = GroupName + ".Appointments.Delete";
        public const string ViewAll = GroupName + ".Appointments.ViewAll";
        public const string ViewOne = GroupName + ".Appointments.ViewOne";
        public const string ViewOwn = GroupName + ".Appointments.ViewOwn"; // AppointmentByDoctorId
        public const string Complete = GroupName + ".Appointments.Complete";// Mark Appointment As Completed
    }

    public static class DoctorSchedules
    {
        public const string Create = GroupName + ".DoctorSchedules.Create";
        public const string Edit = GroupName + ".DoctorSchedules.Edit";
        public const string Delete = GroupName + ".DoctorSchedules.Delete";
        public const string ViewAll = GroupName + ".DoctorSchedules.ViewAll";
        public const string ViewOne = GroupName + ".DoctorSchedules.ViewOne";
        public const string ViewOwn = GroupName + "DoctorSchedules.ViewOwn"; // Doctor's Own Schedule
    }


    public static class Identity
    {
        public const string ManageRoles = GroupName + ".Identity.ManageRoles";
        public const string CreateUsers = GroupName + ".Identity.CreateUsers";
    }


}
