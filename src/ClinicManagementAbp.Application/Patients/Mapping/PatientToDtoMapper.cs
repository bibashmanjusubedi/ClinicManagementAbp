using ClinicManagementAbp.Patients.Dtos;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectMapping;

namespace ClinicManagementAbp.Patients.Mapping
{
    public class PatientToDtoMapper : MapperBase<Patient, PatientDto>
    {
        // Map single entity to DTO
        public override PatientDto Map(Patient source)
        {
            if (source == null) return null;

            return new PatientDto
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email,
                PhoneNumber = source.PhoneNumber,
                DateOfBirth = source.DateOfBirth,
                IsDeleted = source.IsDeleted
            };
        }

        // Map to an existing DTO
        public override void Map(Patient source, PatientDto destination)
        {
            if (source == null || destination == null) return;

            destination.Id = source.Id;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.PhoneNumber = source.PhoneNumber;
            destination.DateOfBirth = source.DateOfBirth;
            destination.IsDeleted = source.IsDeleted;
        }
    }
}
