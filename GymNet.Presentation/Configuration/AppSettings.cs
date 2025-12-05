namespace GymNet.Presentation.Configuration;

public sealed class FirebaseSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
}

public sealed class AppSettings
{
    public FirebaseSettings Firebase { get; set; } = new();
}
