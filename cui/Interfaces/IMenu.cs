using System.Collections.Generic;
using cui.Abstractions;

namespace cui.Interfaces {
    public interface IMenu
    {
        IList<ControlBase> Controls { get; }
        void DrawMenu();
    }
}