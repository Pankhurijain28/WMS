using WMS.Application.DTOs.Auth;
using WMS.Application.Helpers;
using WMS.Application.Interfaces;
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

        // HASH the entered password
        var hashedPassword =
    PasswordHasher.Hash(dto.Password);

        Console.WriteLine("INPUT HASH:");
        Console.WriteLine(hashedPassword);

        Console.WriteLine("DB HASH:");
        Console.WriteLine(user.PasswordHash);

        if (user.PasswordHash != hashedPassword)
            return null;

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
}