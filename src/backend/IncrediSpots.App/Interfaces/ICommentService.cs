using IncrediSpots.DataAccess.Entities;
using IncrediSpots.Domain.Models;

namespace IncrediSpots.App.Interfaces;
public interface ICommentService
{
    Task<CommentModel> CreateAsync(Comment comment);
    Task<List<CommentModel>> GetAllCommentsBySpotIdAsync(int spotId);
}
