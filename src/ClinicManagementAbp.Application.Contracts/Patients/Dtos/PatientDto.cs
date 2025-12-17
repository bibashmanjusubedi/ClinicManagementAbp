// ClinicManagementAbp.Application/Patients/Dtos/PatientDto.cs
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace ClinicManagementAbp.Patients.Dtos
{
    /// <summary>
    /// DTO for Patient read operations (GET list/single).
    /// </summary>
    public class PatientDto : FullAuditedEntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsDeleted { get; set; }
    }
}
