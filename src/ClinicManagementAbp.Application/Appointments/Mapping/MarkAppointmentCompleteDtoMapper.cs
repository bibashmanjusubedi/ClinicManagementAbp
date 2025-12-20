using ClinicManagementAbp.Appointments;
using ClinicManagementAbp.Appointments.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Appointments.Mapping
{
    public class MarkAppointmentCompleteMapper : MapperBase<MarkAppointmentCompleteDto, Appointment>
    {
        public override Appointment Map(MarkAppointmentCompleteDto source)
        {
            if (source == null) return null;

            return new Appointment
            {
                Status = AppointmentStatus.Completed,
                Description = source.CompletionNotes // Store notes in description
            };
        }

        public override void Map(MarkAppointmentCompleteDto source, Appointment destination)
        {
            if (source == null || destination == null) return;

            destination.Status = AppointmentStatus.Completed;
            destination.Description = source.CompletionNotes; // Store notes
        }
    }
}
