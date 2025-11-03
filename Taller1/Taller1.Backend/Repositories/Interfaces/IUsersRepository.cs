using Microsoft.AspNetCore.Identity;
using Taller1.Shared.Entities;

namespace Taller1.Backend.Repositories.Interfaces;

public interface IUsersRepository
{
    Task<User> GetUserAsync(string email);

    Task<IdentityResult> AddUserAsync(User user, string password);

    Task CheckRoleAsync(string roleName);

    Task AddUserToRoleAsync(User user, string roleName);

    Task<bool> IsUserInRoleAsync(User user, string roleName);
}