using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    public class Slider : ControlBase, ISlider, ILeftRight, IOtherKey
    {
        /// <summary>
        /// Creates a new instance of <see cref="Slider"/>
        /// </summary>
        /// <param name="name"><inheritdoc cref="ControlBase.Name"/></param>
        protected Slider(string name) : base(name) { }

        /// <inheritdoc cref="Slider"/>
        /// <param name="name"><inheritdoc cref="ControlBase.Name"/></param>
        /// <param name="value"><inheritdoc cref="Value"/></param>
        /// <param name="step"><inheritdoc cref="Step"/></param>
        public Slider(string name, decimal value, decimal step)
            : this(name)
        {
            Value = value;
            Step = step;
        }

        /// <inheritdoc cref="Slider"/>
        /// <param name="name"><inheritdoc cref="ControlBase.Name"/></param>
        /// <param name="value"><inheritdoc cref="Value"/></param>
        /// <param name="step"><inheritdoc cref="Step"/></param>
        /// <param name="min"><inheritdoc cref="Min"/></param>
        /// <param name="max"><inheritdoc cref="Max"/></param>
        public Slider(string name, decimal value, decimal step, decimal min, decimal max)
            : this(name, value, step)
        {
            Min = min;
            Max = max;
            if (min == max || min > max || max < min)
                throw new ArgumentOutOfRangeException(nameof(Min) + " <=> " + nameof(Max));
            
            _hasMinMaxSet = true;
        }

        /// <inheritdoc cref="IHasValue{T}.Value"/>
        public decimal Value
        {
            get => _value;
            set
            {
                if (_hasMinMaxSet && (value > Max || value < Min)) throw new ArgumentException($"Value must be {Min} < Value < {Max}");
                _value = value;
            }
        }
        
        /// <inheritdoc cref="ISlider.Step"/>
        public decimal Step { get; set; }
        
        /// <inheritdoc cref="ISlider.Min"/>
        public decimal Min { get; set; }
        
        /// <inheritdoc cref="ISlider.Max"/>
        public decimal Max { get; set; }

        decimal _value;
        readonly bool _hasMinMaxSet;
        
        /// <inheritdoc cref="ILeftRight.Left"/>
        public void Left(ConsoleKeyInfo info)
        {
            if (_hasMinMaxSet && Min == Value) return;
            Value = (info.Modifiers & ConsoleModifiers.Control) != 0 ? Min : Value -= Step;
        }

        /// <inheritdoc cref="ILeftRight.Right"/>
        public void Right(ConsoleKeyInfo info)
        {
            if (_hasMinMaxSet && Max == Value) return;
            Value = (info.Modifiers & ConsoleModifiers.Control) != 0 ? Max : Value += Step;
        }

        /// <inheritdoc cref="IOtherKey.OtherKey"/>
        public void OtherKey(ConsoleKeyInfo info)
        {
            var key = new ConsoleKeyInfo('\0', ConsoleKey.Clear, false, false, true);
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (info.Key) {
                case ConsoleKey.Home:
                    Left(key);
                    break;
                case ConsoleKey.End:
                    Right(key);
                    break;
            }
        }

        /// <inheritdoc cref="ControlBase.DrawControl"/>
        public override void DrawControl(bool selected)
        {
            Console.Write(Name + " ");
            ConsoleColorHelper.Write("<", ConsoleColor.Cyan);
            ConsoleColorHelper.Write($"{Value}", ConsoleColor.Yellow);
            ConsoleColorHelper.WriteLine(">", ConsoleColor.Cyan);
        }

        /// <inheritdoc cref="ControlBase.GetHash"/>
        protected override int GetHash()
        {
            return Name.GetHashCode() + Value.GetHashCode();
        }
    }
}