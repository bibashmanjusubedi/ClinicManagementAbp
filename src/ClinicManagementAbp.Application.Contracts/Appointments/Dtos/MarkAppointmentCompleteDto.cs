// ClinicManagementAbp.Application.Contracts/Appointments/Dtos/MarkAppointmentCompleteDto.cs
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementAbp.Appointments.Dtos
{
    /// <summary>
    /// Data Transfer Object for marking an appointment as completed.
    /// </summary>
    public class MarkAppointmentCompleteDto
    {
        /// <summary>
        /// Optional notes about the completion of the appointment.
        /// </summary>
        [MaxLength(500)]
        public string? CompletionNotes { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Completed;
    }
}
