using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementAbp.DoctorSchedules.Dtos
{
    /// <summary>
    /// DTO for creating/updating DoctorSchedule entities.
    /// </summary>
    public class CreateUpdateDoctorScheduleDto
    {
        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }
    }
}
