using System.Threading;

namespace cui.Test
{
    static class Program
    {
        static void Main(string[] args)
        {
            var manager = new CuiManager(new CuiSettings
            {
                DisableControlC = true
            });
            
            var menu = new MainMenu();
            new Thread(() =>
            {
                Thread.Sleep(5000);
                menu.Controls[0].Name = "ayylmao";
            }).Start();
            manager.DrawMenu(menu);
        }
    }
}