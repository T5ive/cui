namespace cui.Internal.Helpers;

public static class MenuLogicHelper
{
    internal static void CopyEvents(MenuBase menu)
    {
        foreach (var notify in menu.Controls.Where(c => c is INotifyWhenEnteredExited))
            EventCopyHelper.CopyEventHandlers(menu, notify as INotifyWhenEnteredExited);
    }

    internal static void DrawContents(MenuBase menu)
    {
        NormaliseIndex(menu);
        if (_justWriteMe != null)
        {
            ConsoleColorHelper.WriteLine(menu.Name, ConsoleColor.Yellow);
            WriteMe(_justWriteMe);
        }
        else
        {
            ConsoleColorHelper.WriteLine(menu.Name + Environment.NewLine, ConsoleColor.Yellow);
        }

        for (var i = 0; i < menu.Controls.Count; i++)
        {
            ConsoleColorHelper.Write(menu.Index == i ? "-> " : "   ", ConsoleColor.Cyan);
            menu.Controls[i].DrawControl(menu.Index == i);
        }

        if (_justWriteMeEnd != null)
        {
            WriteMeEnd(_justWriteMeEnd);
        }
    }

    public static Action? _justWriteMe;
    public static Action? _justWriteMeEnd;
    public static void WriteMe(Action method)
    {
        method();
    }
    public static void WriteMeEnd(Action method)
    {
        method();
    }
    internal static void ProcessKey(IMenu menu, ConsoleKeyInfo info)
    {
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (info.Key)
        {
            case ConsoleKey.UpArrow:
                menu.Index--;
                break;
            case ConsoleKey.DownArrow:
                menu.Index++;
                break;
            case ConsoleKey.LeftArrow:
                ControlTriggerHelper.Left(menu, info);
                break;
            case ConsoleKey.RightArrow:
                ControlTriggerHelper.Right(menu, info);
                break;
            case ConsoleKey.Enter:
                ControlTriggerHelper.Press(menu, info);
                break;
            default:
                ControlTriggerHelper.OtherKey(menu, info);
                break;
        }
    }

    private static void NormaliseIndex(IMenu menu)
    {
        if (menu.Index >= menu.Controls.Count)
            menu.Index = 0;
        else if (menu.Index < 0)
            menu.Index = menu.Controls.Count - 1;
    }
}