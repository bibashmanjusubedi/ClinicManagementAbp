using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ClinicManagementAbp.Doctors;
using ClinicManagementAbp.EntityFrameworkCore;

namespace ClinicManagementAbp.Doctors
{
    public class DoctorRepository : EfCoreRepository<ClinicManagementAbpDbContext, Doctor, Guid>, IDoctorRepository
    {
        public DoctorRepository(IDbContextProvider<ClinicManagementAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<bool> DoctorExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).AnyAsync(d => d.Id == id, cancellationToken: cancellationToken);
        }
    }
}
