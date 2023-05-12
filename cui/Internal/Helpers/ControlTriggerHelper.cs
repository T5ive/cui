namespace cui.Internal.Helpers;

internal static class ControlTriggerHelper
{
    internal static void Press(IMenu menu, ConsoleKeyInfo info)
    {
        if (menu.Controls[menu.Index] is IPressable press)
            press.Pressed(info);
    }

    internal static void Left(IMenu menu, ConsoleKeyInfo info)
    {
        if (menu.Controls[menu.Index] is ILeftRight left)
            left.Left(info);
    }

    internal static void Right(IMenu menu, ConsoleKeyInfo info)
    {
        if (menu.Controls[menu.Index] is ILeftRight right)
            right.Right(info);
    }

    internal static void OtherKey(IMenu menu, ConsoleKeyInfo info)
    {
        if (menu.Controls[menu.Index] is IOtherKey other)
            other.OtherKey(info);
    }
}