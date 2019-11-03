using System;
using cui.Interfaces;

namespace cui.Abstractions
{
    /// <summary>
    /// Provides a base class that all controls must inherit
    /// </summary>
    public abstract class ControlBase : IHasName, IControl
    {
        protected ControlBase(string name)
        {
            Name = name;
        }

        public override int GetHashCode() => GetHash();        
        
        /// <summary>
        /// Name of the Control that will be drawn in <see cref="DrawControl"/>
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Calculates the hash code of a control that will be used to check whether it needs re-drawing
        /// </summary>
        /// <returns>Hash code</returns>
        protected virtual int GetHash()
        {
            return Name.GetHashCode();
        }

        /// <summary>
        /// Renders the control as an item of a <see cref="MenuBase"/>
        /// </summary>
        /// <param name="selected">Whether the currently drawn item is selected</param>
        public virtual void DrawControl(bool selected)
        {
            Console.WriteLine(Name);
        }
    }
}