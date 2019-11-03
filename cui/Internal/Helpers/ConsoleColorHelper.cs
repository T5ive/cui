using System;

namespace cui.Internal.Helpers
{
    static class ConsoleColorHelper
    {
        internal static void WriteLine(string text, ConsoleColor color)
        {
            Write(text + Environment.NewLine, color);
        }

        internal static void Write(string text, ConsoleColor color)
        {
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = temp;
        }

        internal static void WriteLine(string text, ConsoleColor foreground, ConsoleColor background)
        {
            Write(text + Environment.NewLine, foreground, background);
        }

        internal static void Write(string text, ConsoleColor foreground, ConsoleColor background)
        {
            var temp = Console.BackgroundColor;
            Console.BackgroundColor = background;
            Write(text, foreground);
            Console.BackgroundColor = temp;
        }

        internal static void Write(char character, ConsoleColor foreground, ConsoleColor background)
        {
            var ftemp = Console.ForegroundColor;
            var btemp = Console.BackgroundColor;
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Write(character);
            Console.ForegroundColor = ftemp;
            Console.BackgroundColor = btemp;
        }
    }
}