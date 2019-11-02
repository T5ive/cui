using cui.Abstractions;
using cui.Interfaces;

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

        readonly ButtonPressedHandler _handler;

        public void Pressed()
        {
            _handler?.Invoke(this);
        }
    }
}