using IncrediSpots.Domain.Models;

namespace IncrediSpots.Domain.Interfaces;
public interface ICommentRepository
{
    Task AddAsync(CommentModel comment);
    Task<List<CommentModel>> GetAllBySpotIdAsync(int spotId);
}
