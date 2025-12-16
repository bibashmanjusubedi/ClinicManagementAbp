using ClinicManagementAbp.Doctors;
using ClinicManagementAbp.Patients;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace ClinicManagementAbp.Appointments
{
    /// <summary>
    /// Enumeration of the possible appointment statuses: Scheduled, Completed, Cancelled.
    /// </summary>
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled
    }

    /// <summary>
    /// Represents an appointment scheduled between a patient and a doctor.
    /// Contains appointment details such as date, description, and status.
    /// </summary>
    public class Appointment : AggregateRoot<Guid>
    {
        /// <summary>
        /// Gets or sets the foreign key referencing the patient.
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key referencing the doctor.
        /// </summary>
        public Guid DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the appointment.
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Gets or sets the description or notes about the appointment.
        /// Maximum length: 500 characters.
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status of the appointment (Scheduled, Completed, Cancelled).
        /// </summary>
        [Required]
        public AppointmentStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the appointment record was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Navigation property to the related patient.
        /// </summary>
        public Patient Patient { get; set; }

        /// <summary>
        /// Navigation property to the related doctor.
        /// </summary>
        public Doctor Doctor { get; set; }
    }
}
