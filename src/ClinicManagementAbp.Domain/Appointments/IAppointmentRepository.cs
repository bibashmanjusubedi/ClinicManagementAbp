using ClinicManagementAbp.Appointments;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ClinicManagementAbp.Appointments
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
        Task<List<Appointment>> GetByDoctorIdAsync(
           Guid doctorId,
           bool includeDetails = true,
           CancellationToken cancellationToken = default);
    }
}