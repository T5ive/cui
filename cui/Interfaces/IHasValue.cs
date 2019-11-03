namespace cui.Interfaces
{
    /// <summary>
    /// Provides an interface for controls that hold a value.
    /// </summary>
    /// <typeparam name="T"><see cref="Value"/></typeparam>
    public interface IHasValue<T>
    {
        /// <summary>
        /// A value of type <typeparamref name="T"/> that the control holds.
        /// </summary>
        T Value { get; set; }
    }
}