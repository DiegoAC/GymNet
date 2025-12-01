using GymNet.Application.Abstractions.Persistence;
using GymNet.Application.Abstractions.Services;
using GymNet.Domain.Common;
using GymNet.Domain.Posts;

namespace GymNet.Application.Posts.CreatePost;

public sealed class CreatePostHandler : ICreatePostHandler
{
    private readonly ICurrentUser _current;
    private readonly IDateTimeProvider _clock;
    private readonly IBlobStorage _blob;
    private readonly IPostsRepository _posts;

    public CreatePostHandler(ICurrentUser current, IDateTimeProvider clock, IBlobStorage blob, IPostsRepository posts)
        => (_current, _clock, _blob, _posts) = (current, clock, blob, posts);

    public async Task<Result<CreatePostResult>> Handle(CreatePostCommand cmd, CancellationToken ct)
    {
        string? mediaUrl = null;
        if (cmd.Media is not null && !string.IsNullOrWhiteSpace(cmd.MediaContentType))
        {
            var path = $"posts/{_current.UserId}/{Guid.NewGuid()}";
            mediaUrl = await _blob.UploadAsync(path, cmd.Media, cmd.MediaContentType!, ct);
        }

        var postRes = Post.Create(_current.UserId, cmd.Text, mediaUrl, _clock.UtcNow);
        if (!postRes.IsSuccess) return Result<CreatePostResult>.Failure(postRes.Error!);

        await _posts.AddAsync(postRes.Value!, ct);
        return Result<CreatePostResult>.Success(new CreatePostResult(postRes.Value!.Id, mediaUrl));
    }
}


