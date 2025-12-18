using ClinicManagementAbp.Patients.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Patients.Mapping
{
    public class PatientCreateUpdateMapper : TwoWayMapperBase<Patient, CreateUpdatePatientDto>
    {
        // Create DTO from entity
        public override CreateUpdatePatientDto Map(Patient source)
        {
            if (source == null) return null;

            return new CreateUpdatePatientDto
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email,
                PhoneNumber = source.PhoneNumber,
                DateOfBirth = source.DateOfBirth
            };
        }

        // Map DTO to a new entity (ReverseMap)
        public override Patient ReverseMap(CreateUpdatePatientDto source)
        {
            if (source == null) return null;

            return new Patient
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email,
                PhoneNumber = source.PhoneNumber,
                DateOfBirth = source.DateOfBirth
            };
        }

        // Map entity to existing DTO
        public override void Map(Patient source, CreateUpdatePatientDto destination)
        {
            if (source == null || destination == null) return;

            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.PhoneNumber = source.PhoneNumber;
            destination.DateOfBirth = source.DateOfBirth;
        }

        // Map DTO to existing entity
        public override void ReverseMap(CreateUpdatePatientDto source, Patient destination)
        {
            if (source == null || destination == null) return;

            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.PhoneNumber = source.PhoneNumber;
            destination.DateOfBirth = source.DateOfBirth;
        }
    }
}
