using System;

namespace cui.Controls
{
    public class Checkbox : Control, IOtherKey, ILeftRight, IPressable
    {
        public Checkbox(string name) : base(name) { }
        
        public bool Checked { get; protected set; }
        
        public override void DrawControl()
        {
            Console.WriteLine($"{Name} [{(Checked ? "X" : " ")}]");
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