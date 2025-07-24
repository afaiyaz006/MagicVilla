using System.Configuration;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using MagicVilla_API.Controllers;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MagicVilla_API.Repository;

public class UserRepository:IUserRepository
{
    private readonly ApplicationDBContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private string secretKey;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserRepository(
        ApplicationDBContext db,
        IConfiguration configuration,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
    }
    public bool IsUniqueUser(string username)
    {
       
 
        var user =  _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
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
        var user =await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
        bool isValid = await _userManager.CheckPasswordAsync(user,loginRequestDTO.Password);
        if (user == null || isValid == false)
        {
            return null;
        }
        else
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = secretKey;
            Console.WriteLine("Secret key "+secretKey);
            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserName.ToString()),
                    new Claim(ClaimTypes.Role,userRole)
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
                User = _mapper.Map<ApplicationUser,UserDTO>(user),
                Role = roles.FirstOrDefault()
            };
            return loginResponseDTO;
        }
    }

    public async Task<RegistrationResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        var response = new RegistrationResponseDTO();
        Console.WriteLine("Registration invoked...");
        Console.WriteLine(registrationRequestDTO.ToString());

        ApplicationUser user = new()
        {
            UserName = registrationRequestDTO.UserName,
            Email = registrationRequestDTO.UserName,
            NormalizedEmail = registrationRequestDTO.UserName.ToUpper(),
            Name = registrationRequestDTO.Name,
        };

        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
        
            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                }
                Console.WriteLine("User creation complete.");
                await _userManager.AddToRoleAsync(user, "admin");
                var userToReturn = _db.ApplicationUsers
                    .FirstOrDefault(u => u.UserName == registrationRequestDTO.UserName);

                response.Success = true;
                response.User = _mapper.Map<ApplicationUser,UserDTO>(userToReturn);
            }
            else
            {
                Console.WriteLine("User creation failed.");
                response.Success = false;
                response.Errors = result.Errors.Select(e => e.Description).ToList();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error Creating new user");
            response.Success = false;
            response.Errors.Add("Internal server error: " + e.Message);
        }

        return response;
    }

}