using IncrediSpots.Domain.Models;

namespace IncrediSpots.Domain.Interfaces;

public interface IUserRepository  : IRepository<UserModel>
{
    Task<UserModel?> GetByEmailAsync(string email);
}
