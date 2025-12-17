// ClinicManagementAbp.Application/Patients/Dtos/CreateUpdatePatientDto.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementAbp.Patients.Dtos
{
    /// <summary>
    /// DTO for creating/updating Patient entities (POST/PUT).
    /// </summary>
    public class CreateUpdatePatientDto
    {
        [Required(ErrorMessage = "First name is required"), MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required"), MaxLength(100)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format"), MaxLength(255)]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number"), MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
    }
}
