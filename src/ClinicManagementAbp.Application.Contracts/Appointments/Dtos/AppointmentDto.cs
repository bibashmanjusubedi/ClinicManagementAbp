// ClinicManagementAbp.Application.Contracts/Appointments/Dtos/AppointmentDto.cs
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using ClinicManagementAbp.Appointments;

namespace ClinicManagementAbp.Appointments.Dtos
{
    /// <summary>
    /// DTO for Appointment read operations (GET list/single).
    /// </summary>
    public class AppointmentDto : FullAuditedEntityDto<Guid>
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }  // Computed from Patient
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; }   // Computed from Doctor
        public DateTime AppointmentDate { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public AppointmentStatus Status { get; set; }
    }
}
