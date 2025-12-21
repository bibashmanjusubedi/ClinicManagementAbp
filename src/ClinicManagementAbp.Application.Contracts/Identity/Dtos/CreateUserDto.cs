using System.ComponentModel.DataAnnotations;


namespace ClinicManagementAbp.Identity.Dtos;

public class CreateUserDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string? RoleName { get; set; }  // Optional - defaults to Receptionist
}