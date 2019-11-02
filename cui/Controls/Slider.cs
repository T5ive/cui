using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    public class Slider<T> : ControlBase, ILeftRight
    {
        public Slider(string name) : base(name) { }

        public Slider(string name, T value)
            : this(name)
        {
            Value = value;
        }

        public Slider(string name, T value, T step)
            : this(name, value)
        {
            Step = step;
        }

        public Slider(string name, T value, T step, T min, T max)
            : this(name, value, step)
        {
            Min = min;
            Max = max;
            _hasMinMaxSet = true;
        }
        
        public T Value { get; set; }
        public T Step { get; set; }
        public T Min { get; set; }
        public T Max { get; set; }

        readonly bool _hasMinMaxSet;
        readonly Func<T, T, T> _add = OperatorHelper.FindAddition<T>();
        readonly Func<T, T, T> _sub = OperatorHelper.FindSubtraction<T>();
        readonly Func<T, T, bool> _lessThan = OperatorHelper.FindLessThan<T>();
        readonly Func<T, T, bool> _greaterThan = OperatorHelper.FindGreaterThan<T>();
        
        public void Left(ConsoleKeyInfo info)
        {
            Value = (info.Modifiers & ConsoleModifiers.Control) != 0 ? Min : _sub(Value, Step);
            if (_hasMinMaxSet && _lessThan(Value, Max)) Value = Min;
        }

        public void Right(ConsoleKeyInfo info)
        {
            Value = (info.Modifiers & ConsoleModifiers.Control) != 0 ? Max : _add(Value, Step);
            if (_hasMinMaxSet && _greaterThan(Value, Max)) Value = Max;
        }

        public override void DrawControl()
        {
            Console.Write(Name + " ");
            ConsoleColorHelper.Write("<", ConsoleColor.Cyan);
            ConsoleColorHelper.Write($"{Value}", ConsoleColor.Yellow);
            ConsoleColorHelper.WriteLine(">", ConsoleColor.Cyan);
        }

        protected override int GetHash()
        {
            return Name.GetHashCode() + Value.GetHashCode();
        }
    }
}