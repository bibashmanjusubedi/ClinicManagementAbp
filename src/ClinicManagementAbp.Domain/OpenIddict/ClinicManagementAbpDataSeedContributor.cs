using System;
using System.Data;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using Volo.Abp.PermissionManagement;


public class ClinicManagementAbpDataSeedContributor : IDataSeedContributor
{
    private readonly IRepository<Role, Guid> _roleRepository;
    private readonly IRepository<User, Guid> _userRepository;
    private readonly IdentityUserManager _userManager;
    private readonly IdentityRoleManager _roleManager;
    private readonly IPermissionGrantRepository _permissionGrantRepository; 

    public ClinicManagementAbpDataSeedContributor(
        IRepository<Role, Guid> roleRepository,
        IRepository<User, Guid> userRepository,
        IdentityUserManager userManager,
        IdentityRoleManager roleManager,
        IPermissionGrantRepository permissionGrantRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _permissionGrantRepository = permissionGrantRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        // Create Roles
        var adminRole = await CreateRoleIfNotExistsAsync("Admin");
        var doctorRole = await CreateRoleIfNotExistsAsync("Doctor");
        var receptionistRole = await CreateRoleIfNotExistsAsync("Receptionist");

        // Assign permissions to roles
        await AssignPermissionsToRoleAsync(adminRole, new[]
        {
            // Admin gets all permissions
            ClinicManagementAbpPermissions.Patients.Create,
            ClinicManagementAbpPermissions.Patients.Edit,
            ClinicManagementAbpPermissions.Patients.SoftDelete,
            ClinicManagementAbpPermissions.Patients.ViewAll,
            ClinicManagementAbpPermissions.Patients.ViewOne,
            ClinicManagementAbpPermissions.Doctors.Create,
            ClinicManagementAbpPermissions.Doctors.Edit,
            ClinicManagementAbpPermissions.Doctors.Delete,
            ClinicManagementAbpPermissions.Doctors.ViewAll,
            ClinicManagementAbpPermissions.Doctors.ViewOne,
            ClinicManagementAbpPermissions.Appointments.Create,
            ClinicManagementAbpPermissions.Appointments.Edit,
            ClinicManagementAbpPermissions.Appointments.Delete,
            ClinicManagementAbpPermissions.Appointments.ViewAll,
            ClinicManagementAbpPermissions.Appointments.ViewOne,
            ClinicManagementAbpPermissions.Appointments.ViewOwn,
            ClinicManagementAbpPermissions.Appointments.Complete,
            ClinicManagementAbpPermissions.DoctorSchedules.Cancel,
            ClinicManagementAbpPermissions.DoctorSchedules.Reschedule,
            ClinicManagementAbpPermissions.DoctorSchedules.Create,
            ClinicManagementAbpPermissions.DoctorSchedules.Edit,
            ClinicManagementAbpPermissions.DoctorSchedules.Delete,
            ClinicManagementAbpPermissions.DoctorSchedules.ViewAll,
            ClinicManagementAbpPermissions.DoctorSchedules.ViewOne,
            ClinicManagementAbpPermissions.DoctorSchedules.ViewOwn,
            ClinicManagementAbpPermissions.Identity.CreateUsers,
            ClinicManagementAbpPermissions.Identity.ManageRoles
        });

        await AssignPermissionsToRoleAsync(doctorRole, new[]
        {
            ClinicManagementAbpPermissions.Patients.ViewAll,
            ClinicManagementAbpPermissions.Appointments.ViewOwn,
            ClinicManagementAbpPermissions.Appointments.Complete
        });

        await AssignPermissionsToRoleAsync(receptionistRole, new[]
        {
            ClinicManagementAbpPermissions.Patients.Create,
            ClinicManagementAbpPermissions.Patients.ViewAll,
            ClinicManagementAbpPermissions.Patients.ViewOne,
            ClinicManagementAbpPermissions.Appointments.Create,
            ClinicManagementAbpPermissions.Appointments.Reschedule,
            ClinicManagementAbpPermissions.Appointments.Cancel,
            ClinicManagementAbpPermissions.DoctorSchedules.ViewAll,
            ClinicManagementAbpPermissions.DoctorSchedules.ViewOne
        });

        // Create sample users
        await CreateUserIfNotExistsAsync("admin@clinic.com", "Admin123!", new[] { "Admin" });
    }

    private async Task<Role> CreateRoleIfNotExistsAsync(string roleName)
    {
        var role = await _roleRepository.FirstOrDefaultAsync(r => r.Name == roleName);
        if (role == null)
        {
            role = await _roleManager.CreateAsync(new IdentityRole(GuidGenerator.Instance.Create(), roleName));
            await _roleRepository.InsertAsync(role);
        }
        return role;
    }

    private async Task AssignPermissionsToRoleAsync(Role role, string[] permissions)// what should be in here
    {
        // Use IPermissionGrantRepository to assign permissions
        // Implementation details depend on your ABP version
        foreach (var permission in permissions)
        {
            var existingGrant = await _permissionGrantRepository.FirstOrDefaultAsync(x =>
                x.HolderId == role.Id &&
                x.HolderType == "role" &&
                x.Name == permission);

            if (existingGrant == null)
            {
                var permissionGrant = new PermissionGrant(
                    GuidGenerator.Instance.Create(),
                    permission,
                    role.Id,
                    "role"
                );
                await _permissionGrantRepository.InsertAsync(permissionGrant);
            }
        }

    }

    private async Task CreateUserIfNotExistsAsync(string email, string password, string[] roles)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = await _userManager.CreateAsync(
                new User(GuidGenerator.Instance.Create(), email, email)
                {
                    IsActive = true
                }, password);

            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name!);
                }
            }
        }
    }
}
