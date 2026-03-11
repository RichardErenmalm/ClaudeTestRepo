using ClaudeTestRepo.Application.DTOs;
using ClaudeTestRepo.Application.Interfaces;
using MediatR;

namespace ClaudeTestRepo.Application.Commands.Auth;

public record LoginCommand(LoginDto Dto) : IRequest<AuthResponseDto?>;

public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Dto.Username);
        if (user == null) return null;

        if (!BCrypt.Net.BCrypt.Verify(request.Dto.Password, user.PasswordHash))
            return null;

        return new AuthResponseDto
        {
            Token = _tokenService.GenerateToken(user),
            Username = user.Username
        };
    }
}
