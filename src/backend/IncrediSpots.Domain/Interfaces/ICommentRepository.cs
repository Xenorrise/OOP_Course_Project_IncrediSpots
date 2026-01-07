using IncrediSpots.Domain.Models;

namespace IncrediSpots.Domain.Interfaces;
public interface ICommentRepository : IRepository<CommentModel>
{
    Task<List<CommentModel>> GetAllBySpotIdAsync(int spotId);
}
