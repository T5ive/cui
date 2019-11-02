namespace cui.Interfaces
{
    public delegate void EnterExitHandler(IHasName sender);
    
    public interface INotifyWhenEnteredExited
    {
        event EnterExitHandler OnEntered;
        event EnterExitHandler OnExited;
    }
}