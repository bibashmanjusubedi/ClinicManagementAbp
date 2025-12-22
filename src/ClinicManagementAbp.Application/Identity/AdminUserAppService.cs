using ClinicManagementAbp.Identity.Dtos;
using ClinicManagementAbp.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Identity;


public class AdminUserAppService : ApplicationService, IAdminUserAppService
{
    private readonly IdentityUserManager _userManager;
    private readonly IdentityRoleManager _roleManager;
    private readonly IGuidGenerator _guidGenerator;

    public AdminUserAppService(
        IdentityUserManager userManager,
        IdentityRoleManager roleManager,
         IGuidGenerator guidGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _guidGenerator = guidGenerator;
    }

    [Authorize(ClinicManagementAbpPermissions.Identity.CreateUsers)]
    public async Task<IdentityUserDto> CreateUserAsync(CreateUserDto input)
    {
        // Default to Receptionist if no role specified
        var roleName = input.RoleName ?? "Receptionist";

        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            throw new UserFriendlyException($"Role '{roleName}' does not exist.");
        }

        // 1️⃣ Create the user object
        var newUser = new IdentityUser(_guidGenerator.Create(), input.UserName, input.Email);

        // 2️⃣ Set properties on the user
        newUser.SetIsActive(true);
        newUser.SetEmailConfirmed(true); // ✅ ABP method to confirm email

        // 3️⃣ Create the user in the database with password
        var result = await _userManager.CreateAsync(newUser, input.Password);
        if (!result.Succeeded)
        {
            throw new UserFriendlyException(
                $"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}"
            );
        }

        // 4️⃣ Assign the role
        await _userManager.AddToRoleAsync(newUser, roleName);

        // 5️⃣ Map to DTO
        return ObjectMapper.Map<IdentityUser, IdentityUserDto>(newUser);
    }





    [Authorize(ClinicManagementAbpPermissions.Identity.ManageRoles)]
    public async Task UpdateUserRolesAsync(Guid userId, UpdateUserRolesDto input)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new EntityNotFoundException(typeof(IdentityUser), userId);
        }

        // Remove all existing roles
        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);

        // Add new roles (default: Receptionist if empty)
        var rolesToAssign = input.RoleNames?.Any() == true ? input.RoleNames : new[] { "Receptionist" };

        foreach (var roleName in rolesToAssign)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
