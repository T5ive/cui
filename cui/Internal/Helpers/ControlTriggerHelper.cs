using System;
using cui.Abstractions;
using cui.Interfaces;

namespace cui.Internal.Helpers
{
    static class ControlTriggerHelper
    {
        internal static void Press(MenuBase menu, ConsoleKeyInfo info)
        {
            if (menu.Controls[menu.Index] is IPressable press)
                press.Pressed(info);
        }

        internal static void Left(MenuBase menu, ConsoleKeyInfo info)
        {
            if (menu.Controls[menu.Index] is ILeftRight left)
                left.Left(info);
        }

        internal static void Right(MenuBase menu, ConsoleKeyInfo info)
        {
            if (menu.Controls[menu.Index] is ILeftRight right)
                right.Right(info);
        }

        internal static void OtherKey(MenuBase menu, ConsoleKeyInfo info)
        {
            if (menu.Controls[menu.Index] is IOtherKey other)
                other.OtherKey(info);
        }
    }
}