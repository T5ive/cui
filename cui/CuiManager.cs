namespace cui;

public class CuiManager
{
    private readonly CuiSettings _settings;
    private readonly Hierarchy _hierarchy = new();

    public CuiManager(CuiSettings? settings = null)
    {
        _settings = settings ?? new CuiSettings();
    }

    public void DrawMenu(MenuBase menu)
    {
        Initialize();
        if (_settings.ShowMenuHierarchyInTitle)
            Tracking_Events(menu);
        (menu as IMenu).DrawMenu();
    }
    private void Initialize()
    {
        if (_settings is { ShowMenuHierarchyInTitle: false, CustomTitle: not null })
            Console.Title = _settings.CustomTitle;

        Console.CursorVisible = _settings.ShowConsoleCursor;
        Console.TreatControlCAsInput = _settings.DisableControlC;
    }

    private void Tracking_Events(INotifyWhenEnteredExited menu)
    {
        menu.OnEntered += _hierarchy.Entered;
        menu.OnExited += _hierarchy.Exited;
    }
}