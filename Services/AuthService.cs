using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Dtos;
using TodoApi.Entities;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
{
    public async Task<User> RegisterAsync(CreateUser createUser)
    {
        if (createUser.Username == null || await userRepository.GetByUsernameAsync(createUser.Username) != null)
            throw new InvalidOperationException("User already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = createUser.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUser.PasswordHash)
        };

        await userRepository.CreateAsync(user);

        return user;
    }

    public async Task<string> LoginAsync(LoginUser loginUser)
    {
        var user = await userRepository.GetByUsernameAsync(loginUser.Username);

        if (user == null)
            throw new InvalidOperationException("User not found");

        if (!BCrypt.Net.BCrypt.Verify(loginUser.PasswordHash, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username!)
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(configuration.GetValue<int>("JwtConfig:TokenValidityMins",30)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}