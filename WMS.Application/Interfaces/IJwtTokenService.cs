using WMS.Domain.Entities;

namespace WMS.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(
        UserLogin user,
        string roleName);
}