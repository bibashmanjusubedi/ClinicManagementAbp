using ClinicManagementAbp.Identity.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace ClinicManagementAbp.Identity;

public interface IAdminUserAppService : IApplicationService
{
    Task<IdentityUserDto> CreateUserAsync(CreateUserDto input);
    Task UpdateUserRolesAsync(Guid userId, UpdateUserRolesDto input);
}
