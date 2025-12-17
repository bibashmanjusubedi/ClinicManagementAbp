using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClinicManagementAbp.Appointments;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ClinicManagementAbp.EntityFrameworkCore
{
    public class AppointmentRepository :
        EfCoreRepository<ClinicManagementAbpDbContext, Appointment, Guid>,
        IAppointmentRepository
    {
        public AppointmentRepository(IDbContextProvider<ClinicManagementAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        public async Task<List<Appointment>> GetByDoctorIdAsync(
            Guid doctorId,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var query = DbSet.Where(x => x.DoctorId == doctorId);

            if (includeDetails)
            {
                query = query
                    .Include(x => x.Patient)
                    .Include(x => x.Doctor);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
