namespace MagicVilla_API.Models.Dto;

public class RegistrationRequestDTO
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}