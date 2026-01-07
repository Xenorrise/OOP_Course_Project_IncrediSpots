using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Models;
using IncrediSpots.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CommentRepository : BaseRepository<CommentModel>, ICommentRepository
{
	public CommentRepository(IncrediSpotsMainDbContext context) : base(context){}

	public override async Task AddAsync(CommentModel comment)
    {
        await base.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CommentModel>> GetAllBySpotIdAsync(int spotId)
    {
        return await _context.Comments
            .Where(c => c.SpotId == spotId)
            .Include(c => c.Author)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }
}
