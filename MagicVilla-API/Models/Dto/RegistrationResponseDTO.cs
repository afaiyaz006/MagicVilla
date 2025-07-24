using MagicVilla_API.Controllers;

namespace MagicVilla_API.Models.Dto;

public class RegistrationResponseDTO
{
    public bool Success { get; set; }
    public UserDTO User { get; set; }
    public List<string> Errors { get; set; } = new();
}
