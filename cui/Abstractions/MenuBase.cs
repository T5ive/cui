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

        bool _needsRedraw;
        int _lastDrawnHash;
        
        public IEnumerable<EnterExitHandler> GetEnteredHandlers() => OnEntered?.GetInvocationList().Cast<EnterExitHandler>();
        public IEnumerable<EnterExitHandler> GetExitedHandlers() => OnExited?.GetInvocationList().Cast<EnterExitHandler>();
        public void Pressed(ConsoleKeyInfo info) => DrawMenu();
        protected void AddControl(ControlBase control) => Controls.Add(control ?? throw new ArgumentNullException(nameof(control)));

        protected void AddControls(IEnumerable<ControlBase> controls)
        {
            foreach (var con in controls) AddControl(con);
        }
        
        public override void DrawControl(bool selected)
        {
            ConsoleColorHelper.Write(Name, ConsoleColor.Yellow);
            ConsoleColorHelper.WriteLine(" >>", ConsoleColor.Cyan);
        }

        public void DrawMenu()
        {
            OnEntered?.Invoke(this);
            MenuLogicHelper.CopyEvents(this);
            _needsRedraw = true;

            while (true)
            {
                if (!_needsRedraw)
                {
                    //Only check the hashes if we are not already redrawing
                    _needsRedraw = HashHelper.NeedsToRedraw(_lastDrawnHash, this);
                }
                
                if (_needsRedraw)
                {
                    Console.Clear();
                    MenuLogicHelper.DrawContents(this);                    

                    _needsRedraw = false;
                    _lastDrawnHash = HashHelper.MakeHash(this);
                }

                if (!Console.KeyAvailable) continue;
                
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape) break;

                MenuLogicHelper.ProcessKey(this, key);
                _needsRedraw = true;
            }

            OnExited?.Invoke(this);
        }
    }
}