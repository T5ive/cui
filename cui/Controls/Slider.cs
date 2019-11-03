using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    /// <summary>
    /// Implements a Slider control, that can contain any type, as long as there is an operator for + and -
    /// or can be incremented/decremented with low-level IL <see cref="OperatorHelper"/>
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Slider{T}.Value"/></typeparam>
    public class Slider<T> : ControlBase, ISlider<T>, ILeftRight, IOtherKey where T : IEquatable<T>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Slider{T}"/>
        /// </summary>
        /// <param name="name"><inheritdoc cref="ControlBase.Name"/></param>
        public Slider(string name) : base(name) { }

        /// <inheritdoc cref="Slider{T}"/>
        /// <param name="name"><inheritdoc cref="ControlBase.Name"/></param>
        /// <param name="value"><inheritdoc cref="Value"/></param>
        /// <param name="step"><inheritdoc cref="Step"/></param>
        public Slider(string name, T value, T step)
            : this(name)
        {
            Value = value;
            Step = step;
        }

        /// <inheritdoc cref="Slider{T}"/>
        /// <param name="name"><inheritdoc cref="ControlBase.Name"/></param>
        /// <param name="value"><inheritdoc cref="Value"/></param>
        /// <param name="step"><inheritdoc cref="Step"/></param>
        /// <param name="min"><inheritdoc cref="Min"/></param>
        /// <param name="max"><inheritdoc cref="Max"/></param>
        public Slider(string name, T value, T step, T min, T max)
            : this(name, value, step)
        {
            Min = min;
            Max = max;
            if (Min.Equals(Max) || _greaterThan(Min, Max) || _lessThan(Max, Min))
                throw new ArgumentOutOfRangeException(nameof(Min) + " and " + nameof(Max));
            
            _hasMinMaxSet = true;
        }
        
        /// <inheritdoc cref="IHasValue{T}.Value"/>
        public T Value { get; set; }
        
        /// <inheritdoc cref="ISlider{T}.Step"/>
        public T Step { get; set; }
        
        /// <inheritdoc cref="ISlider{T}.Min"/>
        public T Min { get; set; }
        
        /// <inheritdoc cref="ISlider{T}.Max"/>
        public T Max { get; set; }

        readonly bool _hasMinMaxSet;
        readonly Func<T, T, T> _add = OperatorHelper.FindAddition<T>();
        readonly Func<T, T, T> _sub = OperatorHelper.FindSubtraction<T>();
        readonly Func<T, T, bool> _lessThan = OperatorHelper.FindLessThan<T>();
        readonly Func<T, T, bool> _greaterThan = OperatorHelper.FindGreaterThan<T>();
        
        /// <inheritdoc cref="ILeftRight.Left"/>
        public void Left(ConsoleKeyInfo info)
        {
            Value = (info.Modifiers & ConsoleModifiers.Control) != 0 ? Min : _sub(Value, Step);
            if (_hasMinMaxSet && _lessThan(Value, Min)) Value = Min;
        }

        /// <inheritdoc cref="ILeftRight.Right"/>
        public void Right(ConsoleKeyInfo info)
        {
            Value = (info.Modifiers & ConsoleModifiers.Control) != 0 ? Max : _add(Value, Step);
            if (_hasMinMaxSet && _greaterThan(Value, Max)) Value = Max;
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