using ClinicManagementAbp.DoctorSchedules;
using ClinicManagementAbp.DoctorSchedules.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;
using ClinicManagementAbp.Doctors;

namespace ClinicManagementAbp.DoctorSchedules.Mapping
{
    public class DoctorScheduleByDoctorDtoMapper : MapperBase<DoctorSchedule, DoctorScheduleByDoctorDto>
    {
        // Map entity to DoctorScheduleByDoctorDto (used in GetSchedulesByDoctorIdAsync)
        public override DoctorScheduleByDoctorDto Map(DoctorSchedule source)
        {
            if (source == null) return null;

            return new DoctorScheduleByDoctorDto
            {
                Id = source.Id,
                DoctorId = source.DoctorId,
                DoctorName = source.Doctor?.FullName ?? string.Empty,
                DayOfWeek = source.DayOfWeek,
                StartTime = source.StartTime,
                EndTime = source.EndTime
            };
        }

        // Map entity to existing DoctorScheduleByDoctorDto
        public override void Map(DoctorSchedule source, DoctorScheduleByDoctorDto destination)
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
