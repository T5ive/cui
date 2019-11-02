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
            foreach (var menu in Controls.Where(c => c is MenuBase))
                EventCopyHelper.CopyEventHandlers(this, menu as MenuBase);

            _copiedEvents = true;
        }

        bool _needsRedraw = true;
        int _lastDrawnHash;
        public void DrawMenu()
        {
            OnEntered?.Invoke(this);
            
            if (!_copiedEvents)
                CopyEvents();

            _lastDrawnHash = HashHelper.MakeHash(Controls);
            while (true)
            {
                if (!_needsRedraw) HashHelper.NeedsToRedraw(_lastDrawnHash, Controls);
                if (_needsRedraw)
                {
                    Console.Clear();
                    NormaliseIndex();
                    ConsoleColorHelper.WriteLine(Name + Environment.NewLine, ConsoleColor.Yellow);

                    for (var i = 0; i < Controls.Count; i++)
                    {
                        ConsoleColorHelper.Write(Index == i ? "-> " : "   ", ConsoleColor.Cyan);
                        Controls[i].DrawControl();
                    }

                    _needsRedraw = false;
                    _lastDrawnHash = HashHelper.MakeHash(Controls);
                }

                if (!Console.KeyAvailable) continue;
                
                var key = Console.ReadKey(true);
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
                        ControlLeft(key);
                        break;
                    case ConsoleKey.RightArrow:
                        ControlRight(key);
                        break;
                    case ConsoleKey.Enter:
                        ControlPressed(key);
                        break;
                    default:
                        ControlOtherKey(key);
                        break;
                }

                _needsRedraw = true;
            }

            OnExited?.Invoke(this);
        }

        void ControlOtherKey(ConsoleKeyInfo info)
        {
            if (Controls[Index] is IOtherKey other)
                other.OtherKey(info);
        }

        void ControlPressed(ConsoleKeyInfo info)
        {
            if (Controls[Index] is IPressable pressable)
                pressable.Pressed(info);
        }

        void ControlLeft(ConsoleKeyInfo info)
        {
            if (Controls[Index] is ILeftRight left)
                left.Left(info);
        }

        void ControlRight(ConsoleKeyInfo info)
        {
            if (Controls[Index] is ILeftRight right)
                right.Right(info);
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
        
        public void Pressed(ConsoleKeyInfo info)
        {
            DrawMenu();
        }

        internal IEnumerable<EnterExitHandler> GetEnteredHandlers() => OnEntered?.GetInvocationList().Cast<EnterExitHandler>();
        internal IEnumerable<EnterExitHandler> GetExitedHandlers() => OnExited?.GetInvocationList().Cast<EnterExitHandler>();
        
        public event EnterExitHandler OnEntered;
        public event EnterExitHandler OnExited;
    }
}