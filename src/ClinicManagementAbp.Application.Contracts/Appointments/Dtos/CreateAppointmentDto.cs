// ClinicManagementAbp.Application.Contracts/Appointments/Dtos/CreateUpdateAppointmentDto.cs
using System;
using System.ComponentModel.DataAnnotations;
using ClinicManagementAbp.Appointments;

namespace ClinicManagementAbp.Appointments.Dtos
{
    /// <summary>
    /// DTO for creating/updating Appointment entities (POST/PUT).
    /// </summary>
    public class CreateAppointmentDto
    {
        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    }
}
