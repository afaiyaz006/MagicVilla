using MagicVilla_API.Controllers;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Repository;

public interface IUserRepository
{
    bool IsUniqueUser(string username);
    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    Task<RegistrationResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO);
    
}