using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.IdentityModel.Tokens;

namespace MagicVilla_API.Repository;

public class UserRepository:IUserRepository
{
    private readonly ApplicationDBContext _db;
    private string secretKey;
    public UserRepository(ApplicationDBContext db,IConfiguration configuration)
    {
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
    }
    public bool IsUniqueUser(string username)
    {
        var user =  _db.LocalUsers.FirstOrDefault(x => x.Username == username);
        if (user == null)
            return false;
        return true;
    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        var user = _db.LocalUsers.FirstOrDefault(u => u.Username.ToLower() == loginRequestDTO.UserName.ToLower() && u.Password == loginRequestDTO.Password);
        if (user == null)
        {
            return null;
        }
        else
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires=DateTime.Now.AddHours(2),
                SigningCredentials = new(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;
        }
    }

    public  async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        // throw new NotImplementedException();
        LocalUser user = new LocalUser()
        {
            Username = registrationRequestDTO.UserName,
            Password = registrationRequestDTO.Password,
            Name = registrationRequestDTO.Name,
            Role = registrationRequestDTO.Role,
        };
        _db.LocalUsers.Add(user);
        await _db.SaveChangesAsync();
        user.Password = "";
        return user;
    }
}