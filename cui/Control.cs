namespace cui
{
    public abstract class Control : IHasName
    {
        protected Control(string name)
        {
            Name = name;
        }
        
        public string Name { get; }

        public abstract void DrawControl();
    }
}