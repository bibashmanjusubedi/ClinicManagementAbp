// ClinicManagementAbp.Application.Contracts/Doctors/Dtos/CreateUpdateDoctorDto.cs
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementAbp.Doctors.Dtos
{
    /// <summary>
    /// DTO for creating/updating Doctor entities.
    /// </summary>
    public class CreateUpdateDoctorDto
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; }

        [MaxLength(150)]
        public string Specialization { get; set; }

        [EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Phone, MaxLength(20)]
        public string Phone { get; set; }
    }
}
