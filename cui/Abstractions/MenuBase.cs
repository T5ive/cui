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
        
        public event EnterExitHandler OnEntered;
        public event EnterExitHandler OnExited;
        
        public IList<ControlBase> Controls { get; }
        public int Index { get; set; }

        bool _copiedEvents;
        bool _needsRedraw = true;
        int _lastDrawnHash;
        
        public IEnumerable<EnterExitHandler> GetEnteredHandlers() => OnEntered?.GetInvocationList().Cast<EnterExitHandler>();
        public IEnumerable<EnterExitHandler> GetExitedHandlers() => OnExited?.GetInvocationList().Cast<EnterExitHandler>();
        public void Pressed(ConsoleKeyInfo info) => DrawMenu();
        protected void AddControl(ControlBase control) => Controls.Add(control);

        protected void AddControls(IEnumerable<ControlBase> controls)
        {
            foreach (var con in controls) AddControl(con);
        }
        
        public override void DrawControl()
        {
            ConsoleColorHelper.Write(Name, ConsoleColor.Yellow);
            ConsoleColorHelper.WriteLine(" >>", ConsoleColor.Cyan);
        }

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
                    DrawContents();                    

                    _needsRedraw = false;
                    _lastDrawnHash = HashHelper.MakeHash(Controls);
                }

                if (!Console.KeyAvailable) continue;
                
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape) break;

                ProcessKey(key);
                _needsRedraw = true;
            }

            OnExited?.Invoke(this);
        }

        void DrawContents()
        {
            NormaliseIndex();
            ConsoleColorHelper.WriteLine(Name + Environment.NewLine, ConsoleColor.Yellow);

            for (var i = 0; i < Controls.Count; i++)
            {
                ConsoleColorHelper.Write(Index == i ? "-> " : "   ", ConsoleColor.Cyan);
                Controls[i].DrawControl();
            }
        }

        void ProcessKey(ConsoleKeyInfo info)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (info.Key)
            {
                case ConsoleKey.UpArrow:
                    Index--;
                    break;
                case ConsoleKey.DownArrow:
                    Index++;
                    break;
                case ConsoleKey.LeftArrow:
                    ControlTriggerHelper.Left(this, info);
                    break;
                case ConsoleKey.RightArrow:
                    ControlTriggerHelper.Right(this, info);
                    break;
                case ConsoleKey.Enter:
                    ControlTriggerHelper.Press(this, info);
                    break;
                default:
                    ControlTriggerHelper.OtherKey(this, info);
                    break;
            }
        }
        
        void CopyEvents()
        {
            foreach (var notify in Controls.Where(c => c is INotifyWhenEnteredExited))
                EventCopyHelper.CopyEventHandlers(this, notify as INotifyWhenEnteredExited);

            _copiedEvents = true;
        }

        void NormaliseIndex()
        {
            if (Index >= Controls.Count)
                Index = 0;
            else if (Index < 0)
                Index = Controls.Count - 1;
        }
    }
}