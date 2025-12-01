using CommunityToolkit.Mvvm.ComponentModel;

#pragma warning disable MVVMTK0045

namespace GymNet.Presentation.Models;

public sealed partial class PostComment : ObservableObject
{
    [ObservableProperty] private string id = Guid.NewGuid().ToString("N");
    [ObservableProperty] private string authorName = "Anónimo";
    [ObservableProperty] private DateTime createdAt = DateTime.UtcNow;
    [ObservableProperty] private string text = "";
}

