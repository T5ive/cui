using System;
using System.Collections.Generic;
using System.Linq;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Abstractions
{
    /// <summary>
    /// Provides a base class that all menus must inherit.
    /// </summary>
    public abstract class MenuBase : ControlBase, IMenu, IPressable, INotifyWhenEnteredExited
    {
        protected MenuBase(string name) : base(name)
        {
            Controls = new List<ControlBase>();
        }
        
        /// <inheritdoc cref="INotifyWhenEnteredExited.OnEntered"/>
        public event EnterExitHandler OnEntered;
        
        /// <inheritdoc cref="INotifyWhenEnteredExited.OnExited"/>
        public event EnterExitHandler OnExited;
        
        /// <inheritdoc cref="IMenu.Controls"/>    
        public List<ControlBase> Controls { get; }
        
        /// <inheritdoc cref="IHasIndex.Index"/> 
        public int Index { get; set; }
        
        bool _needsRedraw;
        bool _open = true;
        int _lastDrawnHash;
        
        public IEnumerable<EnterExitHandler> GetEnteredHandlers() => OnEntered?.GetInvocationList().Cast<EnterExitHandler>();
        public IEnumerable<EnterExitHandler> GetExitedHandlers() => OnExited?.GetInvocationList().Cast<EnterExitHandler>();
        public virtual void Pressed(ConsoleKeyInfo info) => DrawMenu();

        public void Close()
        {
            _open = false;
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
            _open = true;

            while (_open)
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