using ClinicManagementAbp.Patients.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Patients
{
    public class PatientAppService:
        CrudAppService<Patient,PatientDto,Guid,
            PagedAndSortedResultRequestDto,CreateUpdatePatientDto>, IPatientAppService
    {
        private readonly IPatientRepository _patientRepository;


        public PatientAppService(
            IRepository<Patient,Guid>.Default repository,
            IPatientRepository patientRepository)
            : base(repository)
        {
            _patientRepository = patientRepository;
        }

        // override standard delete
        public override async Task DeleteAsync(Guid id)
        {
            await _patientRepository.SoftDeleteAsync(id);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            await _patientRepository.SoftDeleteAsync(id);
        }


        public async Task<PagedResultDto<PatientDto>> WithDetailsAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _patientRepository.WithDetailsAsync();

            var totalCount = await AsyncExecuter.CountAsync(queryable);

            queryable = queryable
                .OrderBy(input.Sorting ?? "CreationTime desc")
                .PageBy(input);


            var entities = await AsyncExecuter.ToListAsync(queryable);
            var dtos = ObjectMapper.Map<List<Patient>, List<PatientDto>>(entities);


            return new PagedResultDto<PatientDto>
            {
                TotalCount = totalCount,
                Items = dtos
            };

        }

        public async Task<PatientDto> GetActiveByIdAsync(Guid id)
        {
            var patient = await _patientRepository.GetActiveByIdAsync(id);

            if (patient == null)
            {
                throw new EntityNotFoundException(typeof(Patient), id);
            }

            return ObjectMapper.Map<Patient, PatientDto>(patient);
        }

    }
}
