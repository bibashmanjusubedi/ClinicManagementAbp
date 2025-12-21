using ClinicManagementAbp.Patients.Dtos;
using ClinicManagementAbp.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;


namespace ClinicManagementAbp.Patients
{
    public class PatientAppService:
        CrudAppService<Patient,PatientDto,Guid,
            PagedAndSortedResultRequestDto,CreateUpdatePatientDto>, IPatientAppService
    {
        private readonly IPatientRepository _patientRepository;


        public PatientAppService(IPatientRepository patientRepository)
            
            : base(patientRepository)
        {
            _patientRepository = patientRepository;
        }

        //// override standard delete
        //public override async Task DeleteAsync(Guid id)
        //{
        //    await _patientRepository.SoftDeleteAsync(id);
        //}

        [Authorize(ClinicManagementAbpPermissions.Patients.SoftDelete)]
        public async Task SoftDeleteAsync(Guid id)
        {
            await _patientRepository.SoftDeleteAsync(id);
        }

        [Authorize(ClinicManagementAbpPermissions.Patients.ViewAll)]
        public async Task<PagedResultDto<PatientDto>> WithDetailsAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _patientRepository.WithDetailsAsync();

            var totalCount = await AsyncExecuter.CountAsync(queryable);


            queryable = ApplySorting(queryable, input);
            queryable = ApplyPaging(queryable, input);



            var entities = await AsyncExecuter.ToListAsync(queryable);
            var dtos = ObjectMapper.Map<List<Patient>, List<PatientDto>>(entities);


            return new PagedResultDto<PatientDto>
            {
                TotalCount = totalCount,
                Items = dtos
            };

        }

        [Authorize(ClinicManagementAbpPermissions.Patients.ViewOne)]
        public async Task<PatientDto> GetActiveByIdAsync(Guid id)
        {
            var patient = await _patientRepository.GetActiveByIdAsync(id);

            if (patient == null)
            {
                throw new EntityNotFoundException(typeof(Patient), id);
            }

            return ObjectMapper.Map<Patient, PatientDto>(patient);
        }



        [Authorize(ClinicManagementAbpPermissions.Patients.Create)]
        public override async Task<PatientDto> CreateAsync(CreateUpdatePatientDto input)
        {
            return await base.CreateAsync(input);
        }



        [Authorize(ClinicManagementAbpPermissions.Patients.Edit)]
        public override async Task<PatientDto> UpdateAsync(Guid id, CreateUpdatePatientDto input)
        {
            return await base.UpdateAsync(id, input);
        }


        [Authorize(ClinicManagementAbpPermissions.Patients.ViewOne)]
        public override async Task<PatientDto> GetAsync(Guid id)
        {
            return await base.GetAsync(id);
        }


        [Authorize(ClinicManagementAbpPermissions.Patients.SoftDelete)]
        public override async Task DeleteAsync(Guid id)
        {
            await _patientRepository.SoftDeleteAsync(id);
        }



    }
}
