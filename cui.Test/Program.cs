namespace cui.Test
{
    static class Program
    {
        static void Main(string[] args)
        {
            var manager = new CuiManager(new CuiSettings
            {
                DisableControlC = true,
                CustomTitle = "Hello There"
            });
            
            manager.DrawMenu(new MainMenu());
        }
    }
}