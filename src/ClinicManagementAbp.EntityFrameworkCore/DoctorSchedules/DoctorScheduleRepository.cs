using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ClinicManagementAbp.DoctorSchedules;
using ClinicManagementAbp.EntityFrameworkCore;

namespace ClinicManagementAbp.DoctorSchedules
{
    public class DoctorScheduleRepository : EfCoreRepository<ClinicManagementAbpDbContext, DoctorSchedule, Guid>, IDoctorScheduleRepository
    {
        public DoctorScheduleRepository(IDbContextProvider<ClinicManagementAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

    
        public async Task<IReadOnlyList<DoctorSchedule>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(s => s.DoctorId == doctorId)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
