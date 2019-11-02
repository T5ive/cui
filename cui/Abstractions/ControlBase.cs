using System;
using cui.Interfaces;

namespace cui.Abstractions
{
    public abstract class ControlBase : IHasName, IControl
    {
        protected ControlBase(string name)
        {
            Name = name;
        }
        
        public string Name { get; }

        public virtual void DrawControl()
        {
            Console.WriteLine(Name);
        }
    }
}