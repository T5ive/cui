using System;
using System.Collections.Generic;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Abstractions
{
    public abstract class MenuBase : ControlBase, IMenu, IPressable
    {
        protected MenuBase(string name) : base(name)
        {
            Controls = new List<ControlBase>();
        }

        public void SubscribeToHierarchy(IEnterExitHandler hierarchy)
        {
            if (hierarchy is null)
                return;
            
            _hierarchy = hierarchy;
            OnEntered += hierarchy.Entered;
            OnExited += hierarchy.Exited;
        }

        IEnterExitHandler _hierarchy;
        public IList<ControlBase> Controls { get; }

        protected void AddControl(ControlBase control)
        {
            if (control is MenuBase menu)
            {
                menu.SubscribeToHierarchy(_hierarchy);
            }
            
            Controls.Add(control);
        }
        
        public int Index { get; set; }
        
        public void DrawMenu()
        {
            OnEntered?.Invoke(this);

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
        
        public event EnterExitHandler OnEntered;
        public event EnterExitHandler OnExited;
        
        public void Pressed()
        {
            DrawMenu();
        }
    }
}