using System.Collections.ObjectModel;
using System.Linq;
using GymNet.Presentation.Models;

namespace GymNet.Presentation.Services;

public sealed class FakeFeedStore
{
    public ObservableCollection<FeedPost> Posts { get; } = new();

    public FakeFeedStore()
    {
        Seed();
    }

    private void Seed()
    {
        var coach = new FeedPost
        {
            AuthorName = "Coach Marta",
            CreatedAt = DateTime.UtcNow.AddMinutes(-45),
            Text = "Calienta bien hombros antes de cualquier press. 5-10 minutos marcan la diferencia en tus marcas y en tus articulaciones.",
            IsWorkout = false,
            LikesCount = 5
        };
        coach.Comments.Add(new PostComment
        {
            AuthorName = "Diego",
            CreatedAt = DateTime.UtcNow.AddMinutes(-30),
            Text = "Desde que lo hago, los hombros me molestan mucho menos."
        });
        Posts.Add(coach);

        var diego = new FeedPost
        {
            AuthorName = "Diego",
            CreatedAt = DateTime.UtcNow.AddHours(-2),
            Text = "Pierna terminada: sentadilla 5x5, prensa, femoral tumbado y gemelo. RPE 9, saliendo en modo zombi del gym 🧟‍♂️",
            IsWorkout = true,
            WorkoutSessionType = "Pierna",
            WorkoutRpe = 9,
            LikesCount = 8
        };
        diego.Comments.Add(new PostComment
        {
            AuthorName = "Laura",
            CreatedAt = DateTime.UtcNow.AddHours(-1),
            Text = "Eso es una pierna seria 👏"
        });
        Posts.Add(diego);

        var laura = new FeedPost
        {
            AuthorName = "Laura",
            CreatedAt = DateTime.UtcNow.AddHours(-6),
            Text = "Cardio suave + movilidad. No todo es levantar pesado, cuidar las articulaciones también es progreso 💚",
            IsWorkout = true,
            WorkoutSessionType = "Cardio / movilidad",
            WorkoutRpe = 6,
            LikesCount = 3
        };
        Posts.Add(laura);
    }

    public void AddWorkoutPost(string authorName, string sessionType, int rpe, string notes)
    {
        var text = $"Entreno: {sessionType} • RPE {rpe}\n{notes}";
        Posts.Insert(0, new FeedPost
        {
            AuthorName = string.IsNullOrWhiteSpace(authorName) ? "Tú" : authorName,
            CreatedAt = DateTime.UtcNow,
            Text = text,
            IsWorkout = true,
            WorkoutSessionType = sessionType,
            WorkoutRpe = rpe,
            LikesCount = 0,
            IsLiked = false
        });
    }

    public void AddTextPost(string authorName, string text)
    {
        Posts.Insert(0, new FeedPost
        {
            AuthorName = string.IsNullOrWhiteSpace(authorName) ? "Tú" : authorName,
            CreatedAt = DateTime.UtcNow,
            Text = text,
            IsWorkout = false,
            LikesCount = 0,
            IsLiked = false
        });
    }

    public void ToggleLike(FeedPost? post)
    {
        if (post is null) return;

        if (post.IsLiked)
        {
            post.IsLiked = false;
            if (post.LikesCount > 0)
            {
                post.LikesCount--;
            }
        }
        else
        {
            post.IsLiked = true;
            post.LikesCount++;
        }
    }

    public FeedPost? FindById(string id)
        => Posts.FirstOrDefault(p => p.Id == id);

    public void AddComment(string postId, string authorName, string text)
    {
        var post = FindById(postId);
        if (post is null) return;

        post.Comments.Add(new PostComment
        {
            AuthorName = string.IsNullOrWhiteSpace(authorName) ? "Tú" : authorName,
            CreatedAt = DateTime.UtcNow,
            Text = text
        });
    }
}
