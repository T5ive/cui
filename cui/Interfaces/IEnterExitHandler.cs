namespace cui.Interfaces {
    public interface IEnterExitHandler
    {
        void Entered(IHasName element);
        void Exited(IHasName element);
    }
}