using ClaudeTestRepo.Domain.Models;

namespace ClaudeTestRepo.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User> CreateAsync(User user);
}
