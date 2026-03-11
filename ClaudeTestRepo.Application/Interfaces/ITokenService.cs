using ClaudeTestRepo.Domain.Models;

namespace ClaudeTestRepo.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
