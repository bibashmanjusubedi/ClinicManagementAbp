namespace ClinicManagementAbp.Identity.Dtos;

public class UpdateUserRolesDto
{
    public string[]? RoleNames { get; set; }  // Optional - defaults to Receptionist
}