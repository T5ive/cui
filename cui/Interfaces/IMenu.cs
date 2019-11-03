using System.Collections.Generic;
using cui.Abstractions;

namespace cui.Interfaces {
    public interface IMenu : IHasIndex
    {
        /// <summary>
        /// List of controls that a menu holds
        /// </summary>
        IList<ControlBase> Controls { get; }
        
        /// <summary>
        /// Draw the menu fully
        /// </summary>
        void DrawMenu();
    }
}