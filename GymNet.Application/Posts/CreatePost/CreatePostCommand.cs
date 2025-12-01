using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GymNet.Domain.Common;

namespace GymNet.Application.Posts.CreatePost;

public sealed record CreatePostCommand(string Text, Stream? Media, string? MediaContentType);
public sealed record CreatePostResult(string PostId, string? MediaUrl);

public interface ICreatePostHandler
{
    Task<Result<CreatePostResult>> Handle(CreatePostCommand cmd, CancellationToken ct);
}

