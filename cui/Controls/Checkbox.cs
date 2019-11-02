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

        protected override int GetHash()
        {
            return Checked.GetHashCode() + Name.GetHashCode();
        }

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
                Pressed(info);
        }
        
        public void Left(ConsoleKeyInfo info)
        {
            Pressed(info);
        }

        public void Right(ConsoleKeyInfo info)
        {
            Pressed(info);
        }

        public void Pressed(ConsoleKeyInfo info)
        {
            Checked = !Checked;
        }
    }
}