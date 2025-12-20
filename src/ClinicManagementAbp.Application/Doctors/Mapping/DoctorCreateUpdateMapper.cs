using ClinicManagementAbp.Doctors;
using ClinicManagementAbp.Doctors.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Doctors.Mapping
{
    public class DoctorCreateUpdateMapper : TwoWayMapperBase<Doctor, CreateUpdateDoctorDto>
    {
        // Create DTO from entity
        public override CreateUpdateDoctorDto Map(Doctor source)
        {
            if (source == null) return null;

            return new CreateUpdateDoctorDto
            {
                FullName = source.FullName,
                Specialization = source.Specialization,
                Email = source.Email,
                Phone = source.Phone
            };
        }

        // Map DTO to a new entity (this is what CreateAsync uses)
        public override Doctor ReverseMap(CreateUpdateDoctorDto source)
        {
            if (source == null) return null;

            return new Doctor
            {
                FullName = source.FullName,
                Specialization = source.Specialization,
                Email = source.Email,
                Phone = source.Phone
            };
        }

        // Map entity to existing DTO
        public override void Map(Doctor source, CreateUpdateDoctorDto destination)
        {
            if (source == null || destination == null) return;

            destination.FullName = source.FullName;
            destination.Specialization = source.Specialization;
            destination.Email = source.Email;
            destination.Phone = source.Phone;
        }

        // Map DTO to existing entity
        public override void ReverseMap(CreateUpdateDoctorDto source, Doctor destination)
        {
            if (source == null || destination == null) return;

            destination.FullName = source.FullName;
            destination.Specialization = source.Specialization;
            destination.Email = source.Email;
            destination.Phone = source.Phone;
        }
    }
}
