using ClinicManagementAbp.Appointments;
using ClinicManagementAbp.Appointments.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Appointments.Mapping
{
    public class AppointmentCreateMapper : TwoWayMapperBase<Appointment, CreateAppointmentDto>
    {
        // Create DTO from entity (GET single appointment details)
        public override CreateAppointmentDto Map(Appointment source)
        {
            if (source == null) return null;

            return new CreateAppointmentDto
            {
                PatientId = source.PatientId,
                DoctorId = source.DoctorId,
                AppointmentDate = source.AppointmentDate,
                Description = source.Description,
                Status = source.Status
            };
        }

        // Map DTO to a new entity (POST CreateAsync uses this)
        public override Appointment ReverseMap(CreateAppointmentDto source)
        {
            if (source == null) return null;

            return new Appointment
            {
                PatientId = source.PatientId,
                DoctorId = source.DoctorId,
                AppointmentDate = source.AppointmentDate,
                Description = source.Description,
                Status = source.Status
            };

        }

        // Map entity to existing DTO
        public override void Map(Appointment source, CreateAppointmentDto destination)
        {
            if (source == null || destination == null) return;

            destination.PatientId = source.PatientId;
            destination.DoctorId = source.DoctorId;
            destination.AppointmentDate = source.AppointmentDate;
            destination.Description = source.Description;
            destination.Status = source.Status;
        }

        // Map DTO to existing entity (PUT UpdateAsync uses this)
        public override void ReverseMap(CreateAppointmentDto source, Appointment destination)
        {
            if (source == null || destination == null) return;

            destination.PatientId = source.PatientId;
            destination.DoctorId = source.DoctorId;
            destination.AppointmentDate = source.AppointmentDate;
            destination.Description = source.Description;
            destination.Status = source.Status;
        }
    }
}
