using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using ClinicManagementAbp.Doctors.Dtos;
using Volo.Abp.Application.Dtos;

namespace ClinicManagementAbp.Doctors;

public interface IDoctorAppService: ICrudAppService<
    DoctorDto,
    Guid,
    PagedAndSortedResultRequestDto,
    CreateUpdateDoctorDto,
    CreateUpdateDoctorDto>
{

}