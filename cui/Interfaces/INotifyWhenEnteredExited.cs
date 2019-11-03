using System.Collections.Generic;

namespace cui.Interfaces
{
    public delegate void EnterExitHandler(IHasName sender);
    
    public interface INotifyWhenEnteredExited
    {
        IEnumerable<EnterExitHandler> GetEnteredHandlers();
        IEnumerable<EnterExitHandler> GetExitedHandlers();
        
        /// <summary>
        /// Event fires, when DrawMenu() is called
        /// </summary>
        event EnterExitHandler OnEntered;
        
        /// <summary>
        /// Event fires, when DrawMenu() exits
        /// </summary>
        event EnterExitHandler OnExited;
    }
}