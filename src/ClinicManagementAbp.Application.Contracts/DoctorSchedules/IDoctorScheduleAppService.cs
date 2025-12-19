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
    Task<PagedResultDto<DoctorScheduleDto>> GetDoctorSchedulesByDoctorAsync(Guid doctorId, PagedAndSortedResultRequestDto input);
    Task<PagedResultDto<DoctorScheduleByDoctorDto>> GetDoctorSchedulesByDoctorIdAsync(Guid doctorId, PagedAndSortedResultRequestDto input);
}