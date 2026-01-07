using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Models;
using IncrediSpots.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeApp.DataAccess.Repositories;

public class UserRepository : BaseRepository<UserModel>, IUserRepository
{
    public UserRepository(IncrediSpotsMainDbContext context) : base(context){}

    public Task<UserModel?> GetByEmailAsync(string email)
        => _context.Users.FirstOrDefaultAsync(u => u.Email == email);

	public override async Task<UserModel?> GetByIdAsync(int id)
	{
		var user = await _context.Users.FindAsync(id) ?? throw new Exception($"User with ID {id} not found");
		return user;
	}
    public override async Task AddAsync(UserModel user)
    {
        await base.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
