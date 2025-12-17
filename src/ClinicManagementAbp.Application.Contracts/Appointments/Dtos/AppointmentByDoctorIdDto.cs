// ClinicManagementAbp.Application.Contracts/Appointments/Dtos/AppointmentByDoctorIdDto.cs
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using ClinicManagementAbp.Appointments;

namespace ClinicManagementAbp.Appointments.Dtos
{
    /// <summary>
    /// DTO for appointments filtered by Doctor ID (read-only).
    /// </summary>
    public class AppointmentByDoctorIdDto : EntityDto<Guid>
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public AppointmentStatus Status { get; set; }
    }
}
