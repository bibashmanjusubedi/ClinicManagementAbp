using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using ClinicManagementAbp.Permissions;
using Volo.Abp.DependencyInjection;

namespace ClinicManagementAbp.OpenIddict;

public class ClinicManagementAbpDataSeedContributor : IDataSeedContributor,ITransientDependency
{
    private readonly IRepository<IdentityRole, Guid> _roleRepository;
    private readonly IRepository<IdentityUser, Guid> _userRepository;
    private readonly IdentityUserManager _userManager;
    private readonly IdentityRoleManager _roleManager;
    private readonly IPermissionManager _permissionManager;
    private readonly IGuidGenerator _guidGenerator;


    public ClinicManagementAbpDataSeedContributor(
        IRepository<IdentityRole, Guid> roleRepository,
        IRepository<IdentityUser, Guid> userRepository,
        IdentityUserManager userManager,
        IdentityRoleManager roleManager,
        IPermissionManager permissionManager,
        IGuidGenerator guidGenerator)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _permissionManager = permissionManager;
        _guidGenerator = guidGenerator;
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
            ClinicManagementAbpPermissions.Appointments.Delete,
            ClinicManagementAbpPermissions.Appointments.ViewAll,
            ClinicManagementAbpPermissions.Appointments.ViewOne,
            ClinicManagementAbpPermissions.Appointments.ViewOwn,
            ClinicManagementAbpPermissions.Appointments.Complete,
            ClinicManagementAbpPermissions.Appointments.Cancel,
            ClinicManagementAbpPermissions.Appointments.Reschedule,
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

    private async Task<IdentityRole> CreateRoleIfNotExistsAsync(string roleName)
    {
        var role = await _roleRepository.FirstOrDefaultAsync(r => r.Name == roleName);
        if (role == null)
        {
            role = new IdentityRole(_guidGenerator.Create(), roleName);
            await _roleManager.CreateAsync(role);  // Validates and creates
            //await _roleRepository.InsertAsync(role);  // Persists to DB
        }
        return role;
    }

    private async Task AssignPermissionsToRoleAsync(IdentityRole role, string[] permissions)
    {
        foreach (var permission in permissions)
        {
            await _permissionManager.SetForRoleAsync(role.Name!, permission, true);
        }
    }

    private async Task CreateUserIfNotExistsAsync(string email, string password, string[] roles)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            var newUser = new IdentityUser(
                _guidGenerator.Create(),
                email,
                email
            );

            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                throw new Exception(
                    $"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );
            }

            // Now the user exists
            newUser.SetIsActive(true);
            await _userRepository.UpdateAsync(newUser);

            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(newUser, role.Name!);
                }
            }
        }
    }


}
