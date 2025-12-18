using System.ComponentModel.DataAnnotations;

namespace ClinicManagementAbp.Appointments.Dtos
{
    public class CancelAppointmentDto
    {
        [Required]
        [MaxLength(500)]
        public string CancellationReason { get; set; }
    }
}
