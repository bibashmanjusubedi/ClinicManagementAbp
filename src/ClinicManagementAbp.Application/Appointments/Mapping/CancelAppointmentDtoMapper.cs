using ClinicManagementAbp.Appointments;
using ClinicManagementAbp.Appointments.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Appointments.Mapping
{
    public class CancelAppointmentMapper : MapperBase<CancelAppointmentDto, Appointment>
    {
        public override Appointment Map(CancelAppointmentDto source)
        {
            if (source == null) return null;

            return new Appointment
            {
                Status = AppointmentStatus.Cancelled, // Hard-coded for cancel action
                Description = source.CancellationReason // Store reason in description
            };
        }

        public override void Map(CancelAppointmentDto source, Appointment destination)
        {
            if (source == null || destination == null) return;

            destination.Status = AppointmentStatus.Cancelled; // Set cancelled status
            destination.Description = source.CancellationReason; // Store reason
        }
    }
}
