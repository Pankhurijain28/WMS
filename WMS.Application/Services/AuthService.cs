using WMS.Application.DTOs.Auth;
using WMS.Application.Helpers;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly IJwtTokenService _jwt;

    public AuthService(
        IAuthRepository repository,
        IJwtTokenService jwt)
    {
        _repository = repository;
        _jwt = jwt;
    }

    public async Task<LoginResponseDto?> LoginAsync(
        LoginRequestDto dto)
    {
        var user =
            await _repository.GetByUsernameAsync(
                dto.Username);

        if (user == null)
            return null;

        var hashedPassword =
            PasswordHasher.Hash(dto.Password);

        if (user.PasswordHash != hashedPassword)
            return null;

        // Record last successful login
        user.LastLogin = DateTime.UtcNow;
        await _repository.UpdateAsync(user);

        var token =
            _jwt.GenerateToken(
                user,
                user.Role!.RoleName);

        return new LoginResponseDto
        {
            Username = user.Username,
            Role = user.Role.RoleName,
            Token = token
        };
    }

    public async Task<LoginResponseDto> RegisterAsync(
        RegisterRequestDto dto)
    {
        var exists =
            await _repository.UsernameExistsAsync(
                dto.Username);

        if (exists)
            throw new Exception(
                "Username already exists");

        var user = new UserLogin
        {
            Username = dto.Username,
            PasswordHash =
                PasswordHasher.Hash(dto.Password),
            RoleId = dto.RoleId
        };

        await _repository.AddUserAsync(user);

        // Reload with Role navigation for token generation
        var created =
            await _repository.GetByUsernameAsync(
                dto.Username);

        var token =
            _jwt.GenerateToken(
                created!,
                created.Role!.RoleName);

        return new LoginResponseDto
        {
            Username = created.Username,
            Role = created.Role.RoleName,
            Token = token
        };
    }
}