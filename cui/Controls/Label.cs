using System;

namespace cui.Controls
{
    public class Label : Control
    {
        public Label(string name) : base(name) { }

        public override void DrawControl()
        {
            Console.WriteLine(Name);
        }
    }
}