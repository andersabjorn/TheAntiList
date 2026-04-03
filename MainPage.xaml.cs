namespace TheAntiListApp;


public partial class MainPage : ContentPage
{
    private const string TaskKey = "current_task";

    public MainPage()
    {
        InitializeComponent();
        LoadTask();
    }

    private void LoadTask()
    {
        string task = Preferences.Get(TaskKey, string.Empty);

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
        Preferences.Set(TaskKey, input);
        TaskEntry.Text = string.Empty;
        ShowActiveTask(input);
        AddButton.IsEnabled = true;
    }

    private void OnDoneClicked(object sender, EventArgs e)
    {
        Preferences.Remove(TaskKey);
        ShowEmptyState();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        try
        {
            bool answer = await DisplayAlert("Remove task?", "Are you sure?", "Yes", "No");
            if (answer)
            {
                Preferences.Remove(TaskKey);
                ShowEmptyState();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"OnDeleteClicked error: {ex}");
        }
    }
    

    private void ShowEmptyState()
    {
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