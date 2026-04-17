namespace TheAntiListApp;

public class TaskRepository
{
    private const string TaskKey = "current_task";
    private const string TaskDateKey = "task_date";

    public string? GetTaskText() =>
        Preferences.Get(TaskKey, null);

    public DateOnly? GetTaskDate()
    {
        string? raw = Preferences.Get(TaskDateKey, null);
        return raw != null ? DateOnly.Parse(raw) : null;
    }

    public void Save(string text)
    {
        Preferences.Set(TaskKey, text);
        Preferences.Set(TaskDateKey, DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd"));
    }

    public void Clear()
    {
        Preferences.Remove(TaskKey);
        Preferences.Remove(TaskDateKey);
    }

    public bool IsFromPreviousDay()
    {
        DateOnly? date = GetTaskDate();
        return date != null && date.Value < DateOnly.FromDateTime(DateTime.Today);
    }
}
