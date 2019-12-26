using System;

namespace cui.Internal.Helpers
{
    public static class ConsoleColorHelper
    {
        public static void Write(string text)
        {
            var temp = Console.ForegroundColor;
            Console.Write(text);
            Console.ForegroundColor = temp;
        }

        public static void Write(string text, ConsoleColor color)
        {
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = temp;
        }

        public static void Write(string text, ConsoleColor foreground, ConsoleColor background)
        {
            var temp = Console.BackgroundColor;
            Console.BackgroundColor = background;
            Write(text, foreground);
            Console.BackgroundColor = temp;
        }

        public static void Write(char character, ConsoleColor foreground, ConsoleColor background)
        {
            var fTemp = Console.ForegroundColor;
            var bTemp = Console.BackgroundColor;
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Write(character);
            Console.ForegroundColor = fTemp;
            Console.BackgroundColor = bTemp;
        }

        public static void WriteLine(string text)
        {
            Write(text + Environment.NewLine);
        }
        public static void WriteLine(string text, ConsoleColor color)
        {
            Write(text + Environment.NewLine, color);
        }
        public static void WriteLine(string text, ConsoleColor foreground, ConsoleColor background)
        {
            Write(text + Environment.NewLine, foreground, background);
        }

        public static void Logger(string text, ConsoleColor color=ConsoleColor.White)
        {
            Write(text + Environment.NewLine + Environment.NewLine, color);
        }

    }
}