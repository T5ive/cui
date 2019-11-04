using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal;

namespace cui
{
    public class CuiManager
    {
        public CuiManager(CuiSettings settings = null)
        {
            _settings = settings ?? new CuiSettings();
        }

        readonly CuiSettings _settings;
        readonly Hierarchy _hierarchy = new Hierarchy();

        public void DrawMenu(MenuBase menu)
        {
            Setup();
            if (_settings.ShowMenuHierarchyInTitle)
                Subscribe(menu);
            (menu as IMenu).DrawMenu();
        }

        void Subscribe(INotifyWhenEnteredExited menu)
        {
            menu.OnEntered += _hierarchy.Entered;
            menu.OnExited += _hierarchy.Exited;
        }

        void Setup()
        {
            if (!_settings.ShowMenuHierarchyInTitle)
                Console.Title = _settings.CustomTitle;

            Console.CursorVisible = _settings.ShowConsoleCursor;
            Console.TreatControlCAsInput = _settings.DisableControlC;
        }
    }
}