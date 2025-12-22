using ClinicManagementAbp.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using ClinicManagementAbp.Identity.Dtos;

namespace ClinicManagementAbp.Identity;


public class AdminUserAppService : ApplicationService, IAdminUserAppService
{
    private readonly IdentityUserManager _userManager;
    private readonly IdentityRoleManager _roleManager;

    public AdminUserAppService(
        IdentityUserManager userManager,
        IdentityRoleManager roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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

        var user = await _userManager.CreateAsync(
            new IdentityUser(GuidGenerator.Instance.Create(), input.UserName, input.Email),
            input.Password
        );

        user.EmailConfirmed = true;
        user.IsActive = true;
        await _userManager.UpdateAsync(user);

        // Assign selected role (default: Receptionist)
        await _userManager.AddToRoleAsync(user, roleName);

        return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
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
