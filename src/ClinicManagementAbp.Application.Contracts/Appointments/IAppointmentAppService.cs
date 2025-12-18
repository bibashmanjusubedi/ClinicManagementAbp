using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using ClinicManagementAbp.Appointments.Dtos;

namespace ClinicManagementAbp.Appointments;


public interface  IAppointmentAppService: ICrudAppService<
    AppointmentDto,
    Guid,
    PagedAndSortedResultRequestDto,
    CreateAppointmentDto>
{
    Task<AppointmentByDoctorIdDto> GetAppointmentsByDoctorIdAsync(Guid doctorId, PagedAndSortedResultRequestDto input);
    
    Task CancelAsync(Guid id,CancelAppointmentDto input);
    Task MarkAsCompleteAsync(Guid id, MarkAppointmentCompleteDto input);

    Task RescheduleAsync(Guid id, RescheduleAppointmentDto input);
}