using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    public delegate void ButtonPressedHandler(Button sender);
    
    public class Button : ControlBase, IPressable, IOtherKey
    {
        public Button(string name) : base(name) { }

        public Button(string name, ButtonPressedHandler handler)
            : this(name)
        {
            _handler = handler;
        }

        public override void DrawControl(bool selected)
        {
            ConsoleColorHelper.WriteLine(Name, ConsoleColor.Cyan);
        }

        readonly ButtonPressedHandler _handler;

        public void Pressed(ConsoleKeyInfo info)
        {
            _handler?.Invoke(this);
        }

        public void OtherKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Spacebar)
                Pressed(info);
        }
    }
}