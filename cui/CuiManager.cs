namespace cui
{
    public class CuiManager
    {
        public CuiManager(CuiSettings settings = null)
        {
            _settings = settings ?? new CuiSettings();
        }

        readonly CuiSettings _settings;

        public void DrawMainMenu<T>(T menu) where T : Control, IMenu
        {
            menu.DrawMenu();
        }
    }
}