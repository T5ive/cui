using System;
using cui.Abstractions;
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
            menu.SubscribeToHierarchy(_settings.ShowMenuHierarchyInTitle ? _hierarchy : null);
            menu.DrawMenu();
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