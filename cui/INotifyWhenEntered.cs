namespace cui
{
    public delegate void EnteredHandler(Control sender);
    
    public interface INotifyWhenEntered
    {
        event EnteredHandler OnEntered;
    }
}