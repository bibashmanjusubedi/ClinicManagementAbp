using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ClinicManagementAbp.DoctorSchedules;
using ClinicManagementAbp.DoctorSchedules.Dtos;
using ClinicManagementAbp.Permissions;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ClinicMAnagementAbp.DoctorSchedules;


public class DoctorScheduleAppService: CrudAppService<DoctorSchedule,DoctorScheduleDto,Guid,PagedAndSortedResultRequestDto,CreateUpdateDoctorScheduleDto,CreateUpdateDoctorScheduleDto>,IDoctorScheduleAppService
{
    public DoctorScheduleAppService(IRepository<DoctorSchedule,Guid> repository): base(repository)
    {
    }


    public async Task<PagedResultDto<DoctorScheduleByDoctorDto>> GetDoctorSchedulesByDoctorIdAsync(Guid doctorId,PagedAndSortedResultRequestDto input)
    {
        var query= await Repository.GetQueryableAsync();
        
        query = query.Where(s => s.DoctorId == doctorId);

        var totalCount = await AsyncExecuter.CountAsync(query);

        var entities = await AsyncExecuter.ToListAsync(
              query.PageBy(input.SkipCount, input.MaxResultCount)
        );


        var dtos = ObjectMapper.Map<List<DoctorSchedule>, List<DoctorScheduleByDoctorDto>>(entities);

        return new PagedResultDto<DoctorScheduleByDoctorDto>(totalCount, dtos);

    }


    public override async Task<DoctorScheduleDto> CreateAsync(CreateUpdateDoctorScheduleDto input)
    {
        var entity = ObjectMapper.Map<CreateUpdateDoctorScheduleDto, DoctorSchedule>(input);

        await Repository.InsertAsync(entity, autoSave: true);

        // 👇 RELOAD with navigation property
        var queryable = await Repository.WithDetailsAsync(x => x.Doctor);

        var scheduleWithDoctor = await AsyncExecuter.FirstAsync(
            queryable.Where(x => x.Id == entity.Id)
        );

        return ObjectMapper.Map<DoctorSchedule, DoctorScheduleDto>(scheduleWithDoctor);

    }


    public override async Task<DoctorScheduleDto> UpdateAsync( Guid id, CreateUpdateDoctorScheduleDto input)
    {
        // 1️⃣ Get existing entity
        var entity = await Repository.GetAsync(id);

        // 2️⃣ Map DTO → entity
        ObjectMapper.Map(input, entity);

        // 3️⃣ Save changes
        await Repository.UpdateAsync(entity, autoSave: true);

        // 4️⃣ Reload with navigation property
        var queryable = await Repository.WithDetailsAsync(x => x.Doctor);

        var scheduleWithDoctor = await AsyncExecuter.FirstAsync(
            queryable.Where(x => x.Id == id)
        );

        // 5️⃣ Map to DTO
        return ObjectMapper.Map<DoctorSchedule, DoctorScheduleDto>(scheduleWithDoctor);
    }


    public override async Task<PagedResultDto<DoctorScheduleDto>> GetListAsync(
     PagedAndSortedResultRequestDto input)
    {
        var query = await Repository.WithDetailsAsync(x => x.Doctor);

        query = ApplySorting(query, input);
        query = ApplyPaging(query, input);

        var totalCount = await AsyncExecuter.CountAsync(query);
        var entities = await AsyncExecuter.ToListAsync(query);

        var dtos = ObjectMapper.Map<List<DoctorSchedule>, List<DoctorScheduleDto>>(entities);

        return new PagedResultDto<DoctorScheduleDto>(totalCount, dtos);
    }



    public override async Task<DoctorScheduleDto> GetAsync(Guid id)
    {
        var query = await Repository.WithDetailsAsync(x => x.Doctor);

        var entity = await AsyncExecuter.FirstAsync(
            query.Where(x => x.Id == id)
            );

        return ObjectMapper.Map<DoctorSchedule, DoctorScheduleDto>(entity);
    }




}