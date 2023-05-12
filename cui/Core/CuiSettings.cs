namespace cui.Core;

public class CuiSettings
{
    private string? _customTitle;
    public string? CustomTitle
    {
        get => _customTitle;
        set
        {
            _customTitle = value;
            ShowMenuHierarchyInTitle = string.IsNullOrEmpty(value);
        }
    }

    private bool _showMenuHierarchyInTitle = true;
    public bool ShowMenuHierarchyInTitle
    {
        get => _showMenuHierarchyInTitle;
        set
        {
            _showMenuHierarchyInTitle = value;
            if (value) CustomTitle = null;
        }
    }

    public bool DisableControlC { get; set; } = true;
    public bool ShowConsoleCursor { get; set; } = false;
}
