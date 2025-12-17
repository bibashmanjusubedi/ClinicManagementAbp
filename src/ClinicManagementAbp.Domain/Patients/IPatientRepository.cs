using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ClinicManagementAbp.Patients
{
    public interface IPatientRepository : IRepository<Patient, Guid>
    {
        Task SoftDeleteAsync(Guid id);
        Task<IQueryable<Patient>> WithDetailsAsync();
        Task<Patient> GetActiveByIdAsync(Guid id);
    }
}
