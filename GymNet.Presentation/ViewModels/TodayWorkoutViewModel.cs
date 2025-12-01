using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymNet.Presentation.Services;
using Microsoft.Maui.Controls;

#pragma warning disable MVVMTK0045

namespace GymNet.Presentation.ViewModels;

public sealed class WorkoutSummary
{
    public DateTime Date { get; init; }
    public string SessionType { get; init; } = "";
    public int Rpe { get; init; }
    public string ShortNotes { get; init; } = "";
}

public partial class TodayWorkoutViewModel : ObservableObject
{
    private static readonly string[] PresetTypes =
    [
        "Pecho / Tríceps",
        "Espalda / Bíceps",
        "Pierna",
        "Full body",
        "Cardio / HIIT"
    ];

    private readonly FakeFeedStore _feedStore;

    [ObservableProperty] private DateTime date = DateTime.Today;
    [ObservableProperty] private string sessionType = "Pecho / Tríceps";
    [ObservableProperty] private string notes = "";
    [ObservableProperty] private int perceivedIntensity = 7;
    [ObservableProperty] private bool shareToFeed = true;
    [ObservableProperty] private string? resultMessage;
    [ObservableProperty] private bool isBusy;

    [ObservableProperty]
    private ObservableCollection<WorkoutSummary> recentWorkouts =
        new ObservableCollection<WorkoutSummary>();

    [ObservableProperty] private string? selectedPresetType;

    public IReadOnlyList<string> PresetSessionTypes => PresetTypes;

    public IAsyncRelayCommand SaveCommand { get; }

    public TodayWorkoutViewModel(FakeFeedStore feedStore)
    {
        _feedStore = feedStore;

        SaveCommand = new AsyncRelayCommand(SaveAsync, () => CanSave);

        RecentWorkouts.Add(new WorkoutSummary
        {
            Date = DateTime.Today.AddDays(-1),
            SessionType = "Espalda / Bíceps",
            Rpe = 8,
            ShortNotes = "Dominadas, remo con barra y curls."
        });

        RecentWorkouts.Add(new WorkoutSummary
        {
            Date = DateTime.Today.AddDays(-2),
            SessionType = "Pierna",
            Rpe = 9,
            ShortNotes = "Sentadilla pesada y prensa."
        });

        SelectedPresetType = SessionType;
    }

    public bool CanSave =>
        !IsBusy &&
        !string.IsNullOrWhiteSpace(SessionType) &&
        !string.IsNullOrWhiteSpace(Notes);

    partial void OnSessionTypeChanged(string value)
        => SaveCommand.NotifyCanExecuteChanged();

    partial void OnNotesChanged(string value)
        => SaveCommand.NotifyCanExecuteChanged();

    partial void OnIsBusyChanged(bool value)
        => SaveCommand.NotifyCanExecuteChanged();

    partial void OnSelectedPresetTypeChanged(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            SessionType = value;
        }
    }

    private async Task SaveAsync()
    {
        if (IsBusy) return;

        ResultMessage = null;

        try
        {
            IsBusy = true;

            await Task.Delay(400); // Simulación de latencia

            var summary = new WorkoutSummary
            {
                Date = Date,
                SessionType = SessionType,
                Rpe = PerceivedIntensity,
                ShortNotes = Notes.Length > 80
                    ? Notes[..80] + "..."
                    : Notes
            };

            RecentWorkouts.Insert(0, summary);

            // Añadir post al feed
            _feedStore.AddWorkoutPost("Tú", SessionType, PerceivedIntensity, Notes);

            ResultMessage = "Entreno guardado. ¡Buen trabajo! 💪";

            if (ShareToFeed)
            {
                await Shell.Current.GoToAsync("//feed");
            }
        }
        catch (Exception ex)
        {
            ResultMessage = $"Error al guardar: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
