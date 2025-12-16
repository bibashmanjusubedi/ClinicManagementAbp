using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using ClinicManagementAbp.Appointments;
using ClinicManagementAbp.DoctorSchedules;

namespace ClinicManagementAbp.Doctors
{
    /// <summary>
    /// Represents a doctor in the clinic management system.
    /// Contains personal details and related appointments and schedules.
    /// </summary>
    public class Doctor : AggregateRoot<Guid>
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; }

        [MaxLength(150)]
        public string Specialization { get; set; }

        [EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Phone, MaxLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// Navigation property for the appointments associated with the doctor.
        /// Represents a collection of appointments handled by this doctor.
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; }

        /// <summary>
        /// Navigation property for the doctor's weekly availability schedules.
        /// Represents a collection of schedule entries defining available time slots.
        /// </summary>
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
    }
}
