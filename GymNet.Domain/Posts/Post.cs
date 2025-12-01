using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GymNet.Domain.Common;

namespace GymNet.Domain.Posts;

public sealed class Post
{
    public string Id { get; private set; } = "";
    public string AuthorId { get; private set; } = "";
    public string Text { get; private set; } = "";
    public string? MediaUrl { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    private Post() { }

    private Post(string id, string authorId, string text, string? mediaUrl, DateTime now)
    {
        Id = id; AuthorId = authorId; Text = text; MediaUrl = mediaUrl; CreatedAtUtc = now;
    }

    public static Result<Post> Create(string authorId, string text, string? mediaUrl, DateTime now)
    {
        if (string.IsNullOrWhiteSpace(text) && string.IsNullOrWhiteSpace(mediaUrl))
            return Result<Post>.Failure("El post no puede estar vacío.");
        return Result<Post>.Success(new Post(Guid.NewGuid().ToString("N"), authorId, text.Trim(), mediaUrl, now));
    }
}

