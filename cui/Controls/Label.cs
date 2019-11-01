using System;

namespace cui.Controls
{
    public class Label : Control
    {
        public Label(string name) : base(name) { }

        public override void Draw()
        {
            Console.WriteLine(Name);
        }
    }
}