using System;
using System.Collections.Generic;
using System.Linq;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Abstractions
{
    public abstract class MenuBase : ControlBase, IMenu, IPressable, INotifyWhenEnteredExited
    {
        protected MenuBase(string name) : base(name)
        {
            Controls = new List<ControlBase>();
        }
        public IList<ControlBase> Controls { get; }

        protected void AddControl(ControlBase control)
        {
            Controls.Add(control);
        }

        protected void AddControls(IEnumerable<ControlBase> controls)
        {
            foreach (var con in controls) AddControl(con);
        }
        
        public int Index { get; set; }

        bool _copiedEvents;
        void CopyEvents()
        {
            foreach (var menu in Controls.Where(c => c is MenuBase).Cast<MenuBase>())
                EventCopyHelper.CopyEventHandlers(this, menu);

            _copiedEvents = true;
        }
        
        public void DrawMenu()
        {
            OnEntered?.Invoke(this);
            if (!_copiedEvents)
                CopyEvents();
            
            while (true)
            {
                Console.Clear();
                NormaliseIndex();
                ConsoleColorHelper.WriteLine(Name + Environment.NewLine, ConsoleColor.Yellow);
                
                for (var i = 0; i < Controls.Count; i++)
                {
                    ConsoleColorHelper.Write(Index == i ? "-> " : "   ", ConsoleColor.Cyan);
                    Controls[i].DrawControl();
                }

                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape) break;
                
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        Index--;
                        break;
                    case ConsoleKey.DownArrow:
                        Index++;
                        break;
                    case ConsoleKey.LeftArrow:
                        ControlLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        ControlRight();
                        break;
                    case ConsoleKey.Enter:
                        ControlPressed();
                        break;
                    default:
                        ControlOtherKey(key);
                        break;
                }
            }

            OnExited?.Invoke(this);
        }

        void ControlOtherKey(ConsoleKeyInfo info)
        {
            if (Controls[Index] is IOtherKey other)
                other.OtherKey(info);
        }

        void ControlPressed()
        {
            if (Controls[Index] is IPressable pressable)
                pressable.Pressed();
        }

        void ControlLeft()
        {
            if (Controls[Index] is ILeftRight left)
                left.Left();
        }

        void ControlRight()
        {
            if (Controls[Index] is ILeftRight right)
                right.Right();
        }

        void NormaliseIndex()
        {
            if (Index >= Controls.Count)
                Index = 0;
            else if (Index < 0)
                Index = Controls.Count - 1;
        }

        public override void DrawControl()
        {
            ConsoleColorHelper.Write(Name, ConsoleColor.Yellow);
            ConsoleColorHelper.WriteLine(" >>", ConsoleColor.Cyan);
        }

        internal IEnumerable<EnterExitHandler> GetEnteredHandlers() => OnEntered?.GetInvocationList().Cast<EnterExitHandler>();
        internal IEnumerable<EnterExitHandler> GetExitedHandlers() => OnExited?.GetInvocationList().Cast<EnterExitHandler>();
        
        public event EnterExitHandler OnEntered;
        public event EnterExitHandler OnExited;
        
        public void Pressed()
        {
            DrawMenu();
        }
    }
}