using ClinicManagementAbp.Appointments;
using ClinicManagementAbp.Appointments.Dtos;
using ClinicManagementAbp.Doctors;
using ClinicManagementAbp.Patients;
using ClinicManagementAbp.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;


namespace ClinicManagementAbp.Appointments;


public class AppointmentAppService : 
    CrudAppService<Appointment,AppointmentDto, Guid, PagedAndSortedResultRequestDto, CreateAppointmentDto>, IAppointmentAppService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;

    public AppointmentAppService(
        IRepository<Appointment, Guid> repository,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository)
        : base(repository)
    {
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
    }

    public override async Task<AppointmentDto> CreateAsync(CreateAppointmentDto input)
    {
        var doctor = await _doctorRepository.GetAsync(input.DoctorId);

        if (doctor == null)
        {
            throw new UserFriendlyException($"Doctor with ID {input.DoctorId} does not exist.");
        }


        var patient = await _patientRepository.GetAsync(input.PatientId);

        if (patient == null)
        {
            throw new UserFriendlyException($"Patient with ID {input.PatientId} does not exist.");
        }

        return await base.CreateAsync(input);

    }

    public async Task<AppointmentByDoctorIdDto> GetAppointmentsByDoctorIdAsync(Guid doctorId, PagedAndSortedResultRequestDto input)
    {
        var doctor = await _doctorRepository.GetAsync(doctorId);

        if (doctor == null)
        {
            throw new UserFriendlyException($"Doctor with ID {doctorId} does not exist.");
        }

        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable.Where(x => x.DoctorId == doctorId);

        queryable = ApplySorting(queryable, input);
        queryable = ApplyPaging(queryable, input);
        
        var appointments = await AsyncExecuter.ToListAsync(queryable);

        return ObjectMapper.Map<List<Appointment>, AppointmentByDoctorIdDto>(appointments);
    }

    public async Task CancelAsync(Guid id, CancelAppointmentDto input)
    {
        var appointment = await Repository.GetAsync(id);

        if (appointment == null)
        {
            throw new UserFriendlyException($"Appointment with ID {id} does not exist.");
        }


        if (appointment.Status != AppointmentStatus.Scheduled)
        {
            throw new UserFriendlyException($"Only scheduled appointments can be canceled.");
        }

        appointment.Status = AppointmentStatus.Cancelled;

        await CurrentUnitOfWork.SaveChangesAsync();
    }


    public async Task MarkAsCompleteAsync(Guid id, MarkAppointmentCompleteDto input)
    {
        var appointment = await Repository.GetAsync(id);
        if (appointment == null)
        {
            throw new UserFriendlyException($"Appointment with ID {id} does not exist.");
        }
        if (appointment.Status != AppointmentStatus.Scheduled)
        {
            throw new UserFriendlyException($"Only scheduled appointments can be marked as complete.");
        }

        appointment.Status = AppointmentStatus.Completed;
        await CurrentUnitOfWork.SaveChangesAsync();

    }

    public async Task RescheduleAsync(Guid id, RescheduleAppointmentDto input)
    {
        var appointment = await Repository.GetAsync(id);
        if (appointment == null)
        {
            throw new UserFriendlyException($"Appointment with ID {id} does not exist.");
        }
        if (appointment.Status != AppointmentStatus.Scheduled)
        {
            throw new UserFriendlyException($"Only scheduled appointments can be rescheduled.");
        }

        appointment.Status = AppointmentStatus.Scheduled;

        await CurrentUnitOfWork.SaveChangesAsync();
    }

}


