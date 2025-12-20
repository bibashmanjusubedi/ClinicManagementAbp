using ClinicManagementAbp.Appointments;
using ClinicManagementAbp.Appointments.Dtos;
using System.Collections.Generic;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;
using System.Linq;

namespace ClinicManagementAbp.Appointments.Mapping;

public class AppointmentByDoctorIdDtoMapper : MapperBase<List<Appointment>, List<AppointmentByDoctorIdDto>>
{
    public override List<AppointmentByDoctorIdDto> Map(List<Appointment> source)
    {
        if (source == null) return null;
        return source.Select(a => new AppointmentByDoctorIdDto
        {
            Id = a.Id,
            PatientName = a.Patient?.FirstName + " " + a.Patient?.LastName,
            AppointmentDate = a.AppointmentDate,
            Status = a.Status
        }).ToList();
    }

    public override void Map(List<Appointment> source, List<AppointmentByDoctorIdDto> destination)
    {
        if (source == null || destination == null)
        {
            return;
        }

        destination.Clear();

        foreach (var a in source)
        {
            destination.Add(new AppointmentByDoctorIdDto
            {
                Id = a.Id,
                PatientName = a.Patient?.FirstName + " " + a.Patient?.LastName,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status
            });
        }
    }
}
