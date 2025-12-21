using ClinicManagementAbp.Doctors;
using ClinicManagementAbp.Doctors.Dtos;
using ClinicManagementAbp.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;

namespace ClinicManagementAbp.Doctors;


public class DoctorAppService : CrudAppService<Doctor,DoctorDto,Guid,PagedAndSortedResultRequestDto,CreateUpdateDoctorDto,CreateUpdateDoctorDto>,
    IDoctorAppService
{
    public DoctorAppService(IRepository<Doctor, Guid> repository)
        : base(repository)
    {
    }


    // Admin Only - Full CRUD
    [Authorize(ClinicManagementAbpPermissions.Doctors.Create)]
    public override async Task<DoctorDto> CreateAsync(CreateUpdateDoctorDto input)
    {
        return await base.CreateAsync(input);
    }

    [Authorize(ClinicManagementAbpPermissions.Doctors.Edit)]
    public override async Task<DoctorDto> UpdateAsync(Guid id, CreateUpdateDoctorDto input)
    {
        return await base.UpdateAsync(id, input);
    }

    [Authorize(ClinicManagementAbpPermissions.Doctors.Delete)]  // Add SoftDelete to permissions
    public override async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(id);
    }

    [Authorize(ClinicManagementAbpPermissions.Doctors.ViewAll)]
    public override async Task<PagedResultDto<DoctorDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        return await base.GetListAsync(input);
    }

    [Authorize(ClinicManagementAbpPermissions.Doctors.ViewOne)]
    public override async Task<DoctorDto> GetAsync(Guid id)
    {
        return await base.GetAsync(id);
    }
}