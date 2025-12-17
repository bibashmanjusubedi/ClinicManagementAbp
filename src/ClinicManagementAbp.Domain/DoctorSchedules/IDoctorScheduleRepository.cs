using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using ClinicManagementAbp.DoctorSchedules;

namespace ClinicManagementAbp.DoctorSchedules
{
    public interface IDoctorScheduleRepository : IRepository<DoctorSchedule, Guid>
    {
        Task<IReadOnlyList<DoctorSchedule>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    }
}
