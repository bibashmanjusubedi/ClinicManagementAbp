using ClinicManagementAbp.Appointments;
using ClinicManagementAbp.Appointments.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Appointments.Mapping;

public class AppointmentToDtoMapper : MapperBase<Appointment, AppointmentDto>
{
    public override AppointmentDto Map(Appointment source)
    {
        if (source == null) return null;
        return new AppointmentDto
        {
            Id = source.Id,
            PatientId = source.PatientId,
            DoctorId = source.DoctorId,
            PatientName = source.Patient?.FirstName + " " + source.Patient?.LastName, // if navigation property exists
            DoctorName = source.Doctor?.FullName,
            AppointmentDate = source.AppointmentDate
        };
    }

    public override void Map(Appointment source, AppointmentDto destination)
    {
        if (source == null || destination == null) return;
        destination.Id = source.Id;
        destination.PatientId = source.PatientId;
        destination.DoctorId = source.DoctorId;
        destination.PatientName = source.Patient?.FirstName + " " + source.Patient?.LastName;
        destination.DoctorName = source.Doctor?.FullName;
        destination.AppointmentDate = source.AppointmentDate;
    }
}

