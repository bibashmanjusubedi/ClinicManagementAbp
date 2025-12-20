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

        input.Status = AppointmentStatus.Scheduled;

        return await base.CreateAsync(input);

    }

    public async Task<PagedResultDto<AppointmentByDoctorIdDto>> GetAppointmentsByDoctorIdAsync(
     Guid doctorId,
     PagedAndSortedResultRequestDto input)
    {
        // Get the doctor
        var doctor = await _doctorRepository.GetAsync(doctorId);
        if (doctor == null)
        {
            throw new UserFriendlyException($"Doctor with ID {doctorId} does not exist.");
        }

        // Get all appointments for this doctor
        var queryable = await Repository.GetQueryableAsync();
        queryable = queryable.Where(x => x.DoctorId == doctorId);

        // Apply sorting and paging
        queryable = ApplySorting(queryable, input);
        queryable = ApplyPaging(queryable, input);

        // Execute the query
        var appointments = await AsyncExecuter.ToListAsync(queryable);

        // Get total count before paging
        var totalCount = await AsyncExecuter.CountAsync(
            (await Repository.GetQueryableAsync()).Where(x => x.DoctorId == doctorId)
        );

        // Manual mapping to DTO
        var appointmentDtos = appointments.Select(a => new AppointmentByDoctorIdDto
        {
            Id = a.Id,
            PatientId = a.PatientId,
            DoctorName = doctor?.FullName,
            AppointmentDate = a.AppointmentDate,
            Description = a.Description,
            Status = a.Status
        }).ToList();

        // Fill patient names
        var patientIds = appointments.Select(a => a.PatientId).Distinct().ToList();
        var patients = await _patientRepository.GetListAsync(p => patientIds.Contains(p.Id));

        foreach (var dto in appointmentDtos)
        {
            var patient = patients.FirstOrDefault(p => p.Id == dto.PatientId);
            dto.PatientName = patient == null ? null : $"{patient.FirstName} {patient.LastName}";
        }

        // Return paged result
        return new PagedResultDto<AppointmentByDoctorIdDto>(
            totalCount,
            appointmentDtos
        );
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

        input.Status = AppointmentStatus.Cancelled;

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

        input.Status = AppointmentStatus.Completed;
        await CurrentUnitOfWork.SaveChangesAsync();

    }

    public async Task RescheduleAsync(Guid id, RescheduleAppointmentDto input)
    {
        // Fetch the appointment
        var appointment = await Repository.GetAsync(id);
        if (appointment == null)
        {
            throw new UserFriendlyException($"Appointment with ID {id} does not exist.");
        }

        // Only scheduled appointments can be rescheduled
        if (appointment.Status != AppointmentStatus.Scheduled)
        {
            throw new UserFriendlyException($"Only scheduled appointments can be rescheduled.");
        }

        // Update the appointment date/time
        appointment.AppointmentDate = input.NewDate; 
        appointment.Status = AppointmentStatus.Scheduled; // keep it scheduled

        await Repository.UpdateAsync(appointment);
        await CurrentUnitOfWork.SaveChangesAsync();
    }


    public override async Task<PagedResultDto<AppointmentDto>> GetListAsync(
    PagedAndSortedResultRequestDto input)
    {
        var query = await Repository.GetQueryableAsync();

        query = ApplySorting(query, input);
        query = ApplyPaging(query, input);

        var appointments = await AsyncExecuter.ToListAsync(query);
        var totalCount = await Repository.GetCountAsync();

        var doctorIds = appointments.Select(a => a.DoctorId).Distinct().ToList();
        var patientIds = appointments.Select(a => a.PatientId).Distinct().ToList();

        var doctors = await _doctorRepository.GetListAsync(d => doctorIds.Contains(d.Id));
        var patients = await _patientRepository.GetListAsync(p => patientIds.Contains(p.Id));

        var appointmentDtos = ObjectMapper.Map<List<Appointment>, List<AppointmentDto>>(appointments);

        foreach (var dto in appointmentDtos)
        {
            var doctor = doctors.FirstOrDefault(d => d.Id == dto.DoctorId);
            var patient = patients.FirstOrDefault(p => p.Id == dto.PatientId);

            dto.DoctorName = doctor?.FullName;
            dto.PatientName = patient == null
                ? null
                : $"{patient.FirstName} {patient.LastName}";
        }


        return new PagedResultDto<AppointmentDto>(
            totalCount,
            appointmentDtos
        );
    }

    public override async Task<AppointmentDto> GetAsync(Guid id)
    {
        var appointment = await Repository.GetAsync(id);

        if (appointment == null)
        {
            throw new UserFriendlyException($"Appointment with ID {id} does not exist.");
        }

        var dto = ObjectMapper.Map<Appointment, AppointmentDto>(appointment);

        var doctor = await _doctorRepository.FindAsync(dto.DoctorId);
        var patient = await _patientRepository.FindAsync(dto.PatientId);

        dto.DoctorName = doctor?.FullName;
        dto.PatientName = patient == null
            ? null
            : $"{patient.FirstName} {patient.LastName}";

        return dto;
    }



}


