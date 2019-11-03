using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    public class Checkbox : ControlBase, IHasValue<bool>, IOtherKey, ILeftRight, IPressable
    {
        public Checkbox(string name, bool check = false) : base(name)
        {
            Value = check;
        }
        
        public bool Value { get; set; }

        protected override int GetHash()
        {
            return Value.GetHashCode() + Name.GetHashCode();
        }

        public override void DrawControl(bool selected)
        {
            Console.Write(Name + " ");
            ConsoleColorHelper.Write("[", ConsoleColor.Cyan);
            ConsoleColorHelper.Write(Value ? "X" : " ", ConsoleColor.Yellow);
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
            Value = !Value;
        }
    }
}