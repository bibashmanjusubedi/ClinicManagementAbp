using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClinicManagementAbp.EntityFrameworkCore;
using ClinicManagementAbp.Patients;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ClinicManagementAbp.EntityFrameworkCore.Patients
{
    public class PatientRepository
        : EfCoreRepository<ClinicManagementAbpDbContext, Patient, Guid>,
          IPatientRepository
    {
        public PatientRepository(
            IDbContextProvider<ClinicManagementAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var dbContext = await GetDbContextAsync();

            var patient = await dbContext.Set<Patient>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (patient == null)
            {
                return;
            }

            patient.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<Patient>> WithDetailsAsync()
        {
            var dbContext = await GetDbContextAsync();

            return dbContext.Set<Patient>()
                .Include(p => p.Appointments)
                .Where(p => !p.IsDeleted);
        }

        public async Task<Patient> GetActiveByIdAsync(Guid id)
        {
            var dbContext = await GetDbContextAsync();

            return await dbContext.Set<Patient>()
                .Include(p => p.Appointments)
                .Where(p => !p.IsDeleted && p.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
