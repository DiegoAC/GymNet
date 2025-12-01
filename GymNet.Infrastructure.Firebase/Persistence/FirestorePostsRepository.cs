using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GymNet.Application.Abstractions.Persistence;
using GymNet.Domain.Posts;

namespace GymNet.Infrastructure.Firebase.Persistence;

public sealed class FirestorePostsRepository : IPostsRepository
{
    private readonly List<Post> _mem = new();

    public Task AddAsync(Post post, CancellationToken ct)
    {
        _mem.Add(post);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Post>> GetFeedAsync(DateTime? before, int pageSize, CancellationToken ct)
    {
        var q = _mem.OrderByDescending(p => p.CreatedAtUtc).Take(pageSize).ToList();
        return Task.FromResult((IReadOnlyList<Post>)q);
    }
}

