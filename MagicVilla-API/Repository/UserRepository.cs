using System.Configuration;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using MagicVilla_API.Controllers;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MagicVilla_API.Repository;

public class UserRepository:IUserRepository
{
    private readonly ApplicationDBContext _db;
    private string secretKey;
    private readonly IMapper _mapper;
    public UserRepository(ApplicationDBContext db,IConfiguration configuration,IMapper mapper)
    {
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        _mapper = mapper;
    }
    public bool IsUniqueUser(string username)
    {
        var user =  _db.LocalUsers.FirstOrDefault(x => x.Username == username);
        if (user == null)
        {
            Console.WriteLine("No user with "+username+" was found");

            return true;
        }
        else
        {
            Console.WriteLine("User with "+username+" was found.");
            return false;
        }
    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        Console.WriteLine("Login Service invoked");
        var user =await _db.LocalUsers.FirstOrDefaultAsync(u => u.Username.ToLower() == loginRequestDTO.UserName.ToLower() && u.Password == loginRequestDTO.Password);
        if (user == null)
        {
            return null;
        }
        else
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = secretKey;
            Console.WriteLine("Secret key "+secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires=DateTime.Now.AddHours(2),
                SigningCredentials = new(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<LocalUser,UserDTO>(user)
            };
            return loginResponseDTO;
        }
    }

    public  async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        // throw new NotImplementedException();
        Console.WriteLine("Registration invoked...");
        Console.WriteLine(registrationRequestDTO.ToString());
        LocalUser user = new LocalUser()
        {
            Username = registrationRequestDTO.UserName,
            Password = registrationRequestDTO.Password,
            Name = registrationRequestDTO.Name,
            Role = registrationRequestDTO.Role,
        };
        await _db.LocalUsers.AddAsync(user);
        await _db.SaveChangesAsync();
        user.Password = "";
        return user;
    }
}