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
        
        public string Name { get; set; }

        protected virtual int GetHash()
        {
            return Name.GetHashCode();
        }

        public override int GetHashCode()
        {
            return GetHash();
        }

        public virtual void DrawControl(bool selected)
        {
            Console.WriteLine(Name);
        }
    }
}