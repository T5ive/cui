using cui.Abstractions;
using cui.Interfaces;
using cui.Internal;
using System;

namespace cui;

public class CuiManager
{
    public CuiManager(CuiSettings settings = null)
    {
        _settings = settings ?? new CuiSettings();
    }

    private readonly CuiSettings _settings;
    private readonly Hierarchy _hierarchy = new Hierarchy();

    public void DrawMenu(MenuBase menu)
    {
        Setup();
        if (_settings.ShowMenuHierarchyInTitle)
            Subscribe(menu);
        (menu as IMenu).DrawMenu();
    }

    private void Subscribe(INotifyWhenEnteredExited menu)
    {
        menu.OnEntered += _hierarchy.Entered;
        menu.OnExited += _hierarchy.Exited;
    }

    private void Setup()
    {
        if (!_settings.ShowMenuHierarchyInTitle)
            Console.Title = _settings.CustomTitle;

        Console.CursorVisible = _settings.ShowConsoleCursor;
        Console.TreatControlCAsInput = _settings.DisableControlC;
    }
}