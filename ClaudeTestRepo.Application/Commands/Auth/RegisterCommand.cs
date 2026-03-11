using ClaudeTestRepo.Application.DTOs;
using ClaudeTestRepo.Application.Interfaces;
using ClaudeTestRepo.Domain.Models;
using MediatR;

namespace ClaudeTestRepo.Application.Commands.Auth;

public record RegisterCommand(RegisterDto Dto) : IRequest<AuthResponseDto?>;

public class RegisterHandler : IRequestHandler<RegisterCommand, AuthResponseDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public RegisterHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existing = await _userRepository.GetByUsernameAsync(request.Dto.Username);
        if (existing != null) return null;

        var user = new User
        {
            Username = request.Dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password)
        };

        await _userRepository.CreateAsync(user);

        return new AuthResponseDto
        {
            Token = _tokenService.GenerateToken(user),
            Username = user.Username
        };
    }
}
