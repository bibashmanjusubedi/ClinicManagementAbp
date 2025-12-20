using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using ClinicManagementAbp.DoctorSchedules.Dtos;
using Volo.Abp.Application.Dtos;

namespace ClinicManagementAbp.DoctorSchedules;

public interface IDoctorScheduleAppService: ICrudAppService<
    DoctorScheduleDto,
    Guid,
    PagedAndSortedResultRequestDto,
    CreateUpdateDoctorScheduleDto,
    CreateUpdateDoctorScheduleDto>
{
    Task<PagedResultDto<DoctorScheduleByDoctorDto>> GetDoctorSchedulesByDoctorIdAsync(Guid doctorId, PagedAndSortedResultRequestDto input);
}