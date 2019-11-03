using System.Collections.Generic;

namespace cui.Interfaces
{
    public delegate void EnterExitHandler(IHasName sender);
    
    public interface INotifyWhenEnteredExited
    {
        IEnumerable<EnterExitHandler> GetEnteredHandlers();
        IEnumerable<EnterExitHandler> GetExitedHandlers();
        event EnterExitHandler OnEntered;
        event EnterExitHandler OnExited;
    }
}