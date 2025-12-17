// ClinicManagementAbp.Application.Contracts/Doctors/Dtos/DoctorDto.cs
using System;
using Volo.Abp.Application.Dtos;

namespace ClinicManagementAbp.Doctors.Dtos
{
    /// <summary>
    /// DTO for Doctor read operations (GET list/single).
    /// </summary>
    public class DoctorDto : FullAuditedEntityDto<Guid>
    {
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
