using System;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    /// <summary>
    /// Implements a Slider control, that can contain any type, as long as there is an operator for + and -
    /// or can be incremented/decremented with low-level IL, see <see cref="OperatorHelper"/>
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Slider{T}.Value"/></typeparam>
    public class Slider<T> : ControlBase, ISlider<T>, ILeftRight, IOtherKey
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
            if (EqualsT(min, max) || GreaterThan(Min, Max) || LessThan(Max, Min))
                throw new ArgumentOutOfRangeException(nameof(Min) + " <=> " + nameof(Max));
            
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
        static readonly Func<T, T, T> Add = OperatorHelper.FindAddition<T>();
        static readonly Func<T, T, T> Sub = OperatorHelper.FindSubtraction<T>();
        static readonly Func<T, T, bool> LessThan = OperatorHelper.FindLessThan<T>();
        static readonly Func<T, T, bool> GreaterThan = OperatorHelper.FindGreaterThan<T>();
        static readonly Func<T, T, bool> EqualsT = OperatorHelper.FindEquality<T>();
        
        /// <inheritdoc cref="ILeftRight.Left"/>
        public void Left(ConsoleKeyInfo info)
        {
            if (_hasMinMaxSet && EqualsT(Min, Value)) return;
            Value = (info.Modifiers & ConsoleModifiers.Control) != 0 ? Min : Sub(Value, Step);
        }

        /// <inheritdoc cref="ILeftRight.Right"/>
        public void Right(ConsoleKeyInfo info)
        {
            if (_hasMinMaxSet && EqualsT(Max, Value)) return;
            Value = (info.Modifiers & ConsoleModifiers.Control) != 0 ? Max : Add(Value, Step);
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