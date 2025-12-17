using ClinicManagementAbp.Patients.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ClinicManagementAbp.Patients
{
    public interface IPatientAppService : ICrudAppService<
        PatientDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdatePatientDto>
    {
        Task SoftDeleteAsync(Guid id);
        Task<PagedResultDto<PatientDto>> WithDetailsAsync(PagedAndSortedResultRequestDto input);  // Your WithDetailsAsync()
        Task<PatientDto> GetActiveByIdAsync(Guid id);
    }
}
