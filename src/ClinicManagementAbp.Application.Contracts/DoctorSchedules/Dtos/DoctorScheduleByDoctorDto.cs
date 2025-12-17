using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using System.DayOfWeek;

namespace ClinicManagementAbp.DoctorSchedules.Dtos
{
    /// <summary>
    /// Data Transfer Object for retrieving a doctor's schedule by doctor ID.
    /// </summary>
    public class DoctorScheduleByDoctorDto : EntityDto<Guid>
    {
        public Guid DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DoctorName { get; set; }
    }
}
