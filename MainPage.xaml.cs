namespace TheAntiListApp;

public partial class MainPage : ContentPage
{
    private readonly TaskRepository _repo = new();

    public MainPage()
    {
        InitializeComponent();
        LoadTask();
    }

    private void LoadTask()
    {
        if (_repo.IsFromPreviousDay())
        {
            string? oldTask = _repo.GetTaskText();
            _repo.Clear();
            ShowEmptyState(hint: oldTask);
            return;
        }

        string? task = _repo.GetTaskText();

        if (string.IsNullOrWhiteSpace(task))
        {
            ShowEmptyState();
        }
        else
        {
            ShowActiveTask(task);
        }
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        string input = TaskEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            _ = DisplayAlert("Nope", "Write a task first.", "OK");
            return;
        }

        AddButton.IsEnabled = false;
        _repo.Save(input);
        TaskEntry.Text = string.Empty;
        ShowActiveTask(input);
        AddButton.IsEnabled = true;
    }

    private void OnDoneClicked(object sender, EventArgs e)
    {
        _repo.Clear();
        ShowEmptyState();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        try
        {
            bool answer = await DisplayAlert("Remove task?", "Are you sure?", "Yes", "No");
            if (answer)
            {
                _repo.Clear();
                ShowEmptyState();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"OnDeleteClicked error: {ex}");
        }
    }

    private void ShowEmptyState(string? hint = null)
    {
        TaskEntry.Placeholder = hint != null ? $"Yesterday: {hint}" : "What's your one thing?";
        EmptyStateLabel.IsVisible = true;
        TaskEntry.IsVisible = true;
        AddButton.IsVisible = true;
        TaskLabel.IsVisible = false;
        ActionButtons.IsVisible = false;
        TaskEntry.Focus();
    }

    private void ShowActiveTask(string task)
    {
        EmptyStateLabel.IsVisible = false;
        TaskEntry.IsVisible = false;
        AddButton.IsVisible = false;
        TaskLabel.Text = task;
        TaskLabel.IsVisible = true;
        ActionButtons.IsVisible = true;
    }
}
