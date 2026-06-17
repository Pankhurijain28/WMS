using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IAuthRepository
{
    Task<UserLogin?> GetByUsernameAsync(
        string username);

    Task<bool> UsernameExistsAsync(
        string username);

    Task AddUserAsync(UserLogin user);

    Task UpdateAsync(UserLogin user);
}