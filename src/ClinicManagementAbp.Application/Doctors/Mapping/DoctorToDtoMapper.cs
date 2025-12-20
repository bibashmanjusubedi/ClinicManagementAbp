using ClinicManagementAbp.Doctors;
using ClinicManagementAbp.Doctors.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Doctors.Mapping
{
    public class DoctorToDtoMapper : MapperBase<Doctor, DoctorDto>
    {
        public override DoctorDto Map(Doctor source)
        {
            if (source == null) return null;

            return new DoctorDto
            {
                Id = source.Id,
                FullName = source.FullName,
                Specialization = source.Specialization,
                Email = source.Email,
                Phone = source.Phone,
            };
        }

        public override void Map(Doctor source, DoctorDto destination)
        {
            if (source == null || destination == null) return;

            destination.Id = source.Id;
            destination.FullName = source.FullName;
            destination.Specialization = source.Specialization;
            destination.Email = source.Email;
            destination.Phone = source.Phone;
        }
    }
}
