using System;
using System.Collections.Generic;
using System.Linq;
using cui.Interfaces;

namespace cui.Internal
{
    class Hierarchy
    {
        readonly Stack<IHasName> _menus = new Stack<IHasName>();

        public void Entered(IHasName element)
        {
            if (_menus.Count < 1 || _menus.Peek() != element)
            {
                _menus.Push(element);
            }

            UpdateConsoleTitle();
        }

        public void Exited(IHasName element)
        {
            _menus.Pop();
            UpdateConsoleTitle();
        }

        void UpdateConsoleTitle()
        {
            Console.Title = string.Join(" >> ", _menus.Select(m => m.Name).Reverse());
        }
    }
}