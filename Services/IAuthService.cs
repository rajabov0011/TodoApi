using TodoApi.Dtos;
using TodoApi.Entities;

namespace TodoApi.Services;

public interface IAuthService
{
    Task<User> RegisterAsync(CreateUser createUser);
    Task<string> LoginAsync(LoginUser loginUser);
}