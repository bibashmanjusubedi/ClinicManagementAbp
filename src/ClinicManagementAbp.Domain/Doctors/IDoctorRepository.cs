using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using ClinicManagementAbp.Doctors;

namespace ClinicManagementAbp.Doctors
{
    public interface IDoctorRepository : IRepository<Doctor, Guid>
    {
        Task<bool> DoctorExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
