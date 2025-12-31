using IncrediSpots.Domain.Models;

namespace IncrediSpots.Domain.Interfaces;

public interface IUserRepository
{
    Task<UserModel?> GetByEmailAsync(string email);
	Task<UserModel> GetByIdAsync(int id);
    Task AddAsync(UserModel user);
}
