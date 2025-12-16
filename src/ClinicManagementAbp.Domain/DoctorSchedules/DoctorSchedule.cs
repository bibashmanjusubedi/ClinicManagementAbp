using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using ClinicManagementAbp.Doctors;  // import Doctor entity

namespace ClinicManagementAbp.DoctorSchedules
{
    /// <summary>
    /// Represents a doctor's availability schedule on a specific day of the week and time range.
    /// </summary>
    public class DoctorSchedule : AggregateRoot<Guid>
    {
        /// <summary>
        /// Gets or sets the foreign key referencing the associated doctor.
        /// </summary>
        public Guid DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the day of the week for this schedule.
        /// This is a required field.
        /// </summary>
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the start time of the doctor's availability for the day.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the doctor's availability for the day.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Navigation property to the doctor associated with this schedule.
        /// </summary>
        public Doctor Doctor { get; set; }
    }
}
