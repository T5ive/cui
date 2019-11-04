using System.Collections.Generic;
using cui.Abstractions;

namespace cui.Interfaces {
    public interface IMenu : IHasIndex
    {
        /// <summary>
        /// List of controls that a menu holds
        /// </summary>
        List<ControlBase> Controls { get; } //Sadly we have to use List<T> instead of IList<T>, because IList<T> doesn't have an AddRange() method :/
        
        /// <summary>
        /// Draw the menu fully
        /// </summary>
        void DrawMenu();

        /// <summary>
        /// Closes the menu
        /// </summary>
        void Close();
    }
}