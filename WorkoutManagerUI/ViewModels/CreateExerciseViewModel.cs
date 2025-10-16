using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp1;

namespace WorkoutManagerUI.ViewModels;

public partial class CreateExerciseViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _name = "";

    [ObservableProperty]
    private string _description = "";

    [ObservableProperty]
    private int _difficultyLevel = 1;

    [ObservableProperty]
    private string _primaryFocusInput = "";

    [ObservableProperty]
    private ObservableCollection<string> _primaryFocus = new();

    [ObservableProperty]
    private string _secondaryFocusInput = "";

    [ObservableProperty]
    private ObservableCollection<string> _secondaryFocus = new();

    [ObservableProperty]
    private string _exerciseInformation = "";

    [ObservableProperty]
    private string _executionStepsInput = "";

    [ObservableProperty]
    private ObservableCollection<string> _executionSteps = new();

    [ObservableProperty]
    private string _videoURL = "";

    [ObservableProperty]
    private int _sets = 3;

    [ObservableProperty]
    private int _reps = 12;

    [ObservableProperty]
    private int _restTimeSeconds = 60;

    [ObservableProperty]
    private string _equipmentInput = "";

    [ObservableProperty]
    private ObservableCollection<string> _equipmentNeeded = new();

    public ICommand AddPrimaryFocusCommand { get; }
    public ICommand RemovePrimaryFocusCommand { get; }
    public ICommand AddSecondaryFocusCommand { get; }
    public ICommand RemoveSecondaryFocusCommand { get; }
    public ICommand AddExecutionStepCommand { get; }
    public ICommand RemoveExecutionStepCommand { get; }
    public ICommand AddEquipmentCommand { get; }
    public ICommand RemoveEquipmentCommand { get; }
    public ICommand SaveExerciseCommand { get; }
    public ICommand CancelCommand { get; }

    private readonly Action<bool>? _onComplete;

    public CreateExerciseViewModel(Action<bool>? onComplete = null)
    {
        _onComplete = onComplete;

        AddPrimaryFocusCommand = new RelayCommand(AddPrimaryFocus);
        RemovePrimaryFocusCommand = new RelayCommand<string>(RemovePrimaryFocus);
        AddSecondaryFocusCommand = new RelayCommand(AddSecondaryFocus);
        RemoveSecondaryFocusCommand = new RelayCommand<string>(RemoveSecondaryFocus);
        AddExecutionStepCommand = new RelayCommand(AddExecutionStep);
        RemoveExecutionStepCommand = new RelayCommand<string>(RemoveExecutionStep);
        AddEquipmentCommand = new RelayCommand(AddEquipment);
        RemoveEquipmentCommand = new RelayCommand<string>(RemoveEquipment);
        SaveExerciseCommand = new RelayCommand(SaveExercise);
        CancelCommand = new RelayCommand(Cancel);
    }

    private void AddPrimaryFocus()
    {
        if (!string.IsNullOrWhiteSpace(PrimaryFocusInput))
        {
            PrimaryFocus.Add(PrimaryFocusInput);
            PrimaryFocusInput = "";
        }
    }

    private void RemovePrimaryFocus(string? item)
    {
        if (item != null)
            PrimaryFocus.Remove(item);
    }

    private void AddSecondaryFocus()
    {
        if (!string.IsNullOrWhiteSpace(SecondaryFocusInput))
        {
            SecondaryFocus.Add(SecondaryFocusInput);
            SecondaryFocusInput = "";
        }
    }

    private void RemoveSecondaryFocus(string? item)
    {
        if (item != null)
            SecondaryFocus.Remove(item);
    }

    private void AddExecutionStep()
    {
        if (!string.IsNullOrWhiteSpace(ExecutionStepsInput))
        {
            ExecutionSteps.Add($"Step {ExecutionSteps.Count + 1}. {ExecutionStepsInput}");
            ExecutionStepsInput = "";
        }
    }

    private void RemoveExecutionStep(string? item)
    {
        if (item != null)
            ExecutionSteps.Remove(item);
    }

    private void AddEquipment()
    {
        if (!string.IsNullOrWhiteSpace(EquipmentInput))
        {
            EquipmentNeeded.Add(EquipmentInput);
            EquipmentInput = "";
        }
    }

    private void RemoveEquipment(string? item)
    {
        if (item != null)
            EquipmentNeeded.Remove(item);
    }

    private void SaveExercise()
    {
        var exercise = new Excercise
        {
            Name = Name,
            Description = Description,
            DifficultyLevel = DifficultyLevel,
            PrimaryFocus = PrimaryFocus.ToList(),
            SecondaryFocus = SecondaryFocus.ToList(),
            ExcerciseInformation = ExerciseInformation,
            ExecutionSteps = ExecutionSteps.ToList(),
            VideoURL = string.IsNullOrWhiteSpace(VideoURL) ? "https://www.youtube.com/watch?v=dQw4w9WgXcQ" : VideoURL,
            Sets = Sets,
            Reps = Reps,
            RecommendedRestTime = TimeSpan.FromSeconds(RestTimeSeconds),
            EquipmentNeeded = EquipmentNeeded.ToList()
        };

        ExcerciseRepository.SaveExcercises(exercise);
        _onComplete?.Invoke(true);
    }

    private void Cancel()
    {
        _onComplete?.Invoke(false);
    }
}
