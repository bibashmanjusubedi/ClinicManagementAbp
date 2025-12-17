// ClinicManagementAbp.Application.Contracts/Appointments/Dtos/RescheduleAppointmentDto.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementAbp.Appointments.Dtos
{
    /// <summary>
    /// Data Transfer Object for rescheduling an appointment.
    /// </summary>
    public class RescheduleAppointmentDto
    {
        /// <summary>
        /// New date and time for the appointment.
        /// </summary>
        [Required]
        public DateTime NewDate { get; set; }
    }
}
