using System;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ClinicManagementAbp.Doctors.Dtos;
using ClinicManagementAbp.Doctors;
using Volo.Abp.Application.Dtos;

namespace ClinicManagementAbp.Doctors;


public class DoctorAppService : CrudAppService<Doctor,DoctorDto,Guid,PagedAndSortedResultRequestDto,CreateUpdateDoctorDto,CreateUpdateDoctorDto>,
    IDoctorAppService
{
    public DoctorAppService(IRepository<Doctor, Guid> repository)
        : base(repository)
    {
    }

}