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


}