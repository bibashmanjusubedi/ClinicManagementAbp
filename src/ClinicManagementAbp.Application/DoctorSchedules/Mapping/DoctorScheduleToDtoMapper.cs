using ClinicManagementAbp.DoctorSchedules;
using ClinicManagementAbp.DoctorSchedules.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;
using ClinicManagementAbp.Doctors;

namespace ClinicManagementAbp.DoctorSchedules.Mapping
{
    public class DoctorScheduleToDtoMapper : MapperBase<DoctorSchedule, DoctorScheduleDto>
    {
        // Map entity to DTO (used in GetListAsync, GetAsync)
        public override DoctorScheduleDto Map(DoctorSchedule source)
        {
            if (source == null) return null;

            return new DoctorScheduleDto
            {
                Id = source.Id,
                DoctorId = source.DoctorId,
                DoctorName = source.Doctor?.FullName ?? string.Empty,
                DayOfWeek = source.DayOfWeek,
                StartTime = source.StartTime,
                EndTime = source.EndTime
            };
        }

        // Map entity to existing DTO (used in UpdateAsync responses)
        public override void Map(DoctorSchedule source, DoctorScheduleDto destination)
        {
            if (source == null || destination == null) return;

            destination.DoctorId = source.DoctorId;
            destination.DoctorName = source.Doctor?.FullName ?? string.Empty;
            destination.DayOfWeek = source.DayOfWeek;
            destination.StartTime = source.StartTime;
            destination.EndTime = source.EndTime;
        }
    }
}
