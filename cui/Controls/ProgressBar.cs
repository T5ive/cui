using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    public class ProgressBar : ControlBase, IHasValue<int>
    {
        public ProgressBar(string name) : base(name) { }

        public ProgressBar(string name, int value, char fillChar = '#')
            : this(name)
        {
            if (value < 0 || value > 100) throw new ArgumentException("Value must be an integer between 0 and 100");
            Value = value;
            _fillChar = fillChar;
        }

        /// <inheritdoc cref="IHasValue{T}.Value"/>
        public int Value
        {
            get => _value;
            set
            {
                if (value < 0 || value > 100) return;
                _value = value;
            }
        }
        
        int _value;
        readonly char _fillChar = '#';

        /// <inheritdoc cref="ControlBase.DrawControl"/>
        public override void DrawControl(bool selected)
        {
            Console.Write(Name + " ");
            for (var i = 0; i < 26; i++)
            {
                switch (i) {
                    case 0:
                        ConsoleColorHelper.Write("[", ConsoleColor.Cyan);
                        break;
                    case 25:
                        ConsoleColorHelper.Write("]", ConsoleColor.Cyan);
                        break;
                    default: {
                        var val = Value / 4;
                        if (i < val) ConsoleColorHelper.Write(_fillChar, ConsoleColor.Yellow, Console.BackgroundColor);
                        else Console.Write(" ");
                        break;
                    }
                }
            }
            ConsoleColorHelper.WriteLine(" " + Value + "%", ConsoleColor.Yellow);
        }

        /// <inheritdoc cref="ControlBase.GetHash"/>
        protected override int GetHash()
        {
            return 31 * Name.GetHashCode() + _value.GetHashCode();
        }
    }
}