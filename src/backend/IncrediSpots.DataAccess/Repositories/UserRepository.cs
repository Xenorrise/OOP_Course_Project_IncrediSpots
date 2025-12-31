using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Models;
using IncrediSpots.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeApp.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IncrediSpotsMainDbContext _context;

    public UserRepository(IncrediSpotsMainDbContext context)
    {
        _context = context;
    }

    public Task<UserModel?> GetByEmailAsync(string email)
        => _context.Users.FirstOrDefaultAsync(u => u.Email == email);

	public async Task<UserModel> GetByIdAsync(int id)
	{
		var user = await _context.Users.FindAsync(id) ?? throw new Exception($"User with ID {id} not found");
		return user;
	}
    public async Task AddAsync(UserModel user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}
