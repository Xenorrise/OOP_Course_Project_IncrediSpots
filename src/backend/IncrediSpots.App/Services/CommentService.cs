using IncrediSpots.App.Interfaces;
using IncrediSpots.DataAccess.Entities;
using IncrediSpots.Domain.Interfaces;
using IncrediSpots.Domain.Models;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly ISpotRepository _spotRepository;
    private readonly IUserRepository _userRepository;

    public CommentService(
        ICommentRepository commentRepository,
        ISpotRepository spotRepository,
        IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _spotRepository = spotRepository;
        _userRepository = userRepository;
    }

    public async Task<CommentModel> CreateAsync(Comment comment)
    {
		var spot = await _spotRepository.GetByIdAsync(comment.SpotId);
		var user = await _userRepository.GetByIdAsync(comment.UserId);
        var model = CommentMapper.From(comment, spot, user);

        await _commentRepository.AddAsync(model);

        return model;
    }

    public async Task<List<CommentModel>> GetAllCommentsBySpotIdAsync(int spotId)
    {
        return await _commentRepository.GetAllBySpotIdAsync(spotId);
    }
}

public static class CommentMapper
{
    public static CommentModel From(Comment comment, SpotModel spot, UserModel user)
    {
        return new CommentModel
        (
            comment.Text,
            spot,
            user
		);
    }
}
