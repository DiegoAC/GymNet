
using GymNet.Domain.Posts;

namespace GymNet.Application.Abstractions.Persistence;
public interface IPostsRepository
{
    Task AddAsync(Post post, CancellationToken ct);
    Task<IReadOnlyList<Post>> GetFeedAsync(DateTime? before, int pageSize, CancellationToken ct);
}

