using ClinicManagementAbp.Appointments;
using ClinicManagementAbp.Appointments.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

public class RescheduleAppointmentDtoMapper : TwoWayMapperBase<Appointment, RescheduleAppointmentDto>
{

    public override RescheduleAppointmentDto Map(Appointment source) // Fixed return type
    {
        return new RescheduleAppointmentDto
        {
            NewDate = source.AppointmentDate,
        };
    }

    public override Appointment ReverseMap(RescheduleAppointmentDto source)
    {
        return new Appointment
        {
            AppointmentDate = source.NewDate,
        };
    }

    public override void Map(Appointment source, RescheduleAppointmentDto destination)
    {
        if (source == null || destination == null) return;

        destination.NewDate = source.AppointmentDate;
    }


    public override void ReverseMap(RescheduleAppointmentDto source, Appointment destination)
    {
        if (source == null || destination == null) return;

        destination.AppointmentDate = source.NewDate;
    }
}
