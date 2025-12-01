using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

#pragma warning disable MVVMTK0045

namespace GymNet.Presentation.Models;

public sealed partial class FeedPost : ObservableObject
{
    [ObservableProperty] private string id = Guid.NewGuid().ToString("N");
    [ObservableProperty] private string authorName = "Anónimo";
    [ObservableProperty] private DateTime createdAt = DateTime.UtcNow;
    [ObservableProperty] private string text = "";
    [ObservableProperty] private string? mediaUrl;

    [ObservableProperty] private bool isWorkout;
    [ObservableProperty] private string? workoutSessionType;
    [ObservableProperty] private int? workoutRpe;

    [ObservableProperty] private int likesCount;
    [ObservableProperty] private bool isLiked;

    public ObservableCollection<PostComment> Comments { get; } = new();
}



