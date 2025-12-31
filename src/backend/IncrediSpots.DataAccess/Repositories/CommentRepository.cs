using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Models;
using IncrediSpots.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CommentRepository : ICommentRepository
{
    private readonly IncrediSpotsMainDbContext _context;

    public CommentRepository(IncrediSpotsMainDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CommentModel comment)
    {
        _context.Comments.Add(comment);
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
