using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    public class Checkbox : ControlBase, IOtherKey, ILeftRight, IPressable
    {
        public Checkbox(string name) : base(name) { }
        
        public bool Checked { get; protected set; }
        
        public override void DrawControl()
        {
            Console.Write(Name + " ");
            ConsoleColorHelper.Write("[", ConsoleColor.Cyan);
            ConsoleColorHelper.Write(Checked ? "X" : " ", ConsoleColor.Yellow);
            ConsoleColorHelper.WriteLine("]", ConsoleColor.Cyan);
        }

        public void OtherKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Spacebar)
                Pressed();
        }
        
        public void Left()
        {
            Pressed();
        }

        public void Right()
        {
            Pressed();
        }

        public void Pressed()
        {
            Checked = !Checked;
        }
    }
}