using System.Collections.Generic;
using cui.Abstractions;

namespace cui.Interfaces {
    public interface IMenu : INotifyWhenEnteredExited
    {
        void SubscribeToHierarchy(IEnterExitHandler hierarchy);
        IList<ControlBase> Controls { get; }
        void DrawMenu();
    }
}