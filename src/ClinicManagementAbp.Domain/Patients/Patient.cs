using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using ClinicManagementAbp.Appointments;
using Volo.Abp;

namespace ClinicManagementAbp.Patients
{
    /// <summary>
    /// Represents a patient in the clinic management system.
    /// Contains personal and contact details along with related appointments.
    /// </summary>
    public class Patient : AggregateRoot<Guid>, ISoftDelete
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Phone, MaxLength(20)]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsDeleted { get; set; } 

        /// <summary>
        /// Navigation property for the appointments related to this patient.
        /// Represents a collection of appointments that belong to the patient.
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
