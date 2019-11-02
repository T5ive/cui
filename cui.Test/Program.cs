namespace cui.Test
{
    static class Program
    {
        static void Main(string[] args)
        {
            new CuiManager(new CuiSettings
            {
                DisableControlC = true
            }).DrawMenu(new MainMenu());
        }
    }
}