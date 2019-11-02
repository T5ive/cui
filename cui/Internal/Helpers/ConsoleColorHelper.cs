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
    }
}