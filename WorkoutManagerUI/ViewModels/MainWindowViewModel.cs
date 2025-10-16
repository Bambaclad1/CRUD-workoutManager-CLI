using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConsoleApp1;

namespace WorkoutManagerUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentViewModel;

    [ObservableProperty]
    private ObservableCollection<Excercise> _exercises = new();

    [ObservableProperty]
    private Excercise? _selectedExercise;

    public ICommand ShowHomeCommand { get; }
    public ICommand ShowExercisesCommand { get; }
    public ICommand ShowCreateExerciseCommand { get; }

    public MainWindowViewModel()
    {
        ShowHomeCommand = new RelayCommand(ShowHome);
        ShowExercisesCommand = new RelayCommand(ShowExercises);
        ShowCreateExerciseCommand = new RelayCommand(ShowCreateExercise);
        
        _currentViewModel = new HomeViewModel();
        LoadExercises();
    }

    private void ShowHome()
    {
        CurrentViewModel = new HomeViewModel();
    }

    private void ShowExercises()
    {
        LoadExercises();
        CurrentViewModel = new ExercisesViewModel(Exercises);
    }

    private void ShowCreateExercise()
    {
        CurrentViewModel = new CreateExerciseViewModel(OnCreateExerciseComplete);
    }

    private void OnCreateExerciseComplete(bool success)
    {
        if (success)
        {
            ShowExercises();
        }
    }

    private void LoadExercises()
    {
        try
        {
            var jsonPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "excerciseFormat.json");
            if (System.IO.File.Exists(jsonPath))
            {
                var content = System.IO.File.ReadAllText(jsonPath);
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var exercisesList = System.Text.Json.JsonSerializer.Deserialize<List<Excercise>>(content, new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                        AllowTrailingCommas = true
                    });

                    Exercises.Clear();
                    if (exercisesList != null)
                    {
                        foreach (var exercise in exercisesList)
                        {
                            Exercises.Add(exercise);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading exercises: {ex.Message}");
        }
    }
}

public partial class HomeViewModel : ViewModelBase
{
}

public partial class ExercisesViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Excercise> _exercises;

    public ExercisesViewModel(ObservableCollection<Excercise> exercises)
    {
        _exercises = exercises;
    }
}

