using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs.Auth;
using WMS.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(
        IAuthService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost("debug-token")]
    public IActionResult DebugToken([FromBody] string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);

            return Ok(new
            {
                jwt.Issuer,
                Audiences = jwt.Audiences,
                Claims = jwt.Claims.Select(x => new
                {
                    x.Type,
                    x.Value
                })
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequestDto dto)
    {
        var result =
            await _service.LoginAsync(dto);

        if (result == null)
        {
            return Unauthorized(
                "Invalid username or password");
        }

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequestDto dto)
    {
        var result =
            await _service.RegisterAsync(dto);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("decode")]
    public IActionResult Decode()
    {
        var handler = new JwtSecurityTokenHandler();

        var token = Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");

        var jwt = handler.ReadJwtToken(token);

        return Ok(new
        {
            Issuer = jwt.Issuer,
            Audience = jwt.Audiences,
            Claims = jwt.Claims.Select(x => new
            {
                x.Type,
                x.Value
            })
        });
    }

    [Authorize]
    [HttpGet("headers")]
    public IActionResult Headers()
    {
        return Ok(Request.Headers
            .ToDictionary(
                h => h.Key,
                h => h.Value.ToString()));
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            Username = User.Identity?.Name,

            Role = User.Claims
                .FirstOrDefault(x =>
                    x.Type ==
                    System.Security.Claims.ClaimTypes.Role)
                ?.Value,

            UserId = User.Claims
                .FirstOrDefault(x =>
                    x.Type == "UserId")
                ?.Value
        });
    }
}