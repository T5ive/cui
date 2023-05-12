using cui.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cui.Internal
{
    internal class Hierarchy
    {
        private readonly Stack<IHasName> _menus = new Stack<IHasName>();

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

        private void UpdateConsoleTitle()
        {
            Console.Title = string.Join(" >> ", _menus.Select(m => m.Name).Reverse());
        }
    }
}