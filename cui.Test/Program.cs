using System;
using cui.Internal.Helpers;

namespace cui.Test
{
    static class Program
    {
        static void Main(string[] args)
        {
            MenuLogicHelper._justWriteMe = About;
            MenuLogicHelper._justWriteMeEnd = Bye;
            new CuiManager(new CuiSettings
            {
                DisableControlC = true
            }).DrawMenu(new MainMenu());
        }
        private static void About()
        {
            ConsoleColorHelper.WriteLine("Hello", ConsoleColor.Yellow);
        }

        private static void Bye()
        {
            ConsoleColorHelper.WriteLine("Bye", ConsoleColor.Yellow);
        }
    }
}