using ClinicManagementAbp.DoctorSchedules;
using ClinicManagementAbp.DoctorSchedules.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.DoctorSchedules.Mapping
{
    public class DoctorScheduleCreateUpdateMapper : TwoWayMapperBase<DoctorSchedule, CreateUpdateDoctorScheduleDto>
    {
        // Create DTO from entity
        public override CreateUpdateDoctorScheduleDto Map(DoctorSchedule source)
        {
            if (source == null) return null;

            return new CreateUpdateDoctorScheduleDto
            {
                DoctorId = source.DoctorId,
                DayOfWeek = source.DayOfWeek,
                StartTime = source.StartTime,
                EndTime = source.EndTime
            };
        }

        // Map DTO to a new entity (this is what CreateAsync uses)
        public override DoctorSchedule ReverseMap(CreateUpdateDoctorScheduleDto source)
        {
            if (source == null) return null;

            return new DoctorSchedule
            {
                DoctorId = source.DoctorId,
                DayOfWeek = source.DayOfWeek,
                StartTime = source.StartTime,
                EndTime = source.EndTime
            };
        }

        // Map entity to existing DTO
        public override void Map(DoctorSchedule source, CreateUpdateDoctorScheduleDto destination)
        {
            if (source == null || destination == null) return;

            destination.DoctorId = source.DoctorId;
            destination.DayOfWeek = source.DayOfWeek;
            destination.StartTime = source.StartTime;
            destination.EndTime = source.EndTime;
        }

        // Map DTO to existing entity
        public override void ReverseMap(CreateUpdateDoctorScheduleDto source, DoctorSchedule destination)
        {
            if (source == null || destination == null) return;

            destination.DoctorId = source.DoctorId;
            destination.DayOfWeek = source.DayOfWeek;
            destination.StartTime = source.StartTime;
            destination.EndTime = source.EndTime;
        }
    }
}
