using System;
using System.Linq;
using cui.Abstractions;
using cui.Interfaces;

namespace cui.Internal.Helpers
{
    static class MenuLogicHelper
    {
        internal static void CopyEvents(MenuBase menu)
        {
            foreach (var notify in menu.Controls.Where(c => c is INotifyWhenEnteredExited))
                EventCopyHelper.CopyEventHandlers(menu, notify as INotifyWhenEnteredExited);
        }
        
        internal static void DrawContents(MenuBase menu)
        {
            NormaliseIndex(menu);
            ConsoleColorHelper.WriteLine(menu.Name + Environment.NewLine, ConsoleColor.Yellow);

            for (var i = 0; i < menu.Controls.Count; i++)
            {
                ConsoleColorHelper.Write(menu.Index == i ? "-> " : "   ", ConsoleColor.Cyan);
                menu.Controls[i].DrawControl(menu.Index == i);
            }
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
        
        static void NormaliseIndex(IMenu menu)
        {
            if (menu.Index >= menu.Controls.Count)
                menu.Index = 0;
            else if (menu.Index < 0)
                menu.Index = menu.Controls.Count - 1;
        }
    }
}