using System;

namespace cui.Controls
{
    public delegate void ButtonPressedHandler(Button sender);
    
    public class Button : Control, IPressable
    {
        public Button(string name) : base(name) { }

        public Button(string name, ButtonPressedHandler handler)
            : this(name)
        {
            _handler = handler;
        }

        readonly ButtonPressedHandler _handler;
        
        public override void DrawControl()
        {
            Console.WriteLine(Name);
        }

        public void Pressed()
        {
            _handler?.Invoke(this);
        }
    }
}