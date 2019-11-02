using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    public delegate void ButtonPressedHandler(Button sender);
    
    public class Button : ControlBase, IPressable
    {
        public Button(string name) : base(name) { }

        public Button(string name, ButtonPressedHandler handler)
            : this(name)
        {
            _handler = handler;
        }

        public override void DrawControl()
        {
            ConsoleColorHelper.Write("[ ", ConsoleColor.Cyan);
            ConsoleColorHelper.Write(Name, ConsoleColor.White);
            ConsoleColorHelper.WriteLine(" ]", ConsoleColor.Cyan);
        }

        readonly ButtonPressedHandler _handler;

        public void Pressed()
        {
            _handler?.Invoke(this);
        }
    }
}