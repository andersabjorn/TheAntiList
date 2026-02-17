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
        string input = TaskEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            DisplayAlert("Nope", "Write a task first.", "OK");
            return;
        }

        Preferences.Set(TaskKey, input);
        TaskEntry.Text = string.Empty;
        ShowActiveTask(input);
    }

    private void OnDoneClicked(object sender, EventArgs e)
    {
        Preferences.Remove(TaskKey);
        ShowEmptyState();
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        Preferences.Remove(TaskKey);
        ShowEmptyState();
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
