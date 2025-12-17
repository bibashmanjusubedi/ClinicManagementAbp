// ClinicManagementAbp.Application.Contracts/DoctorSchedules/Dtos/DoctorScheduleDto.cs
using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementAbp.DoctorSchedules.Dtos
{
    /// <summary>
    /// DTO for DoctorSchedule read operations (GET list/single).
    /// </summary>
    public class DoctorScheduleDto : FullAuditedEntityDto<Guid>
    {
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } 
        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
