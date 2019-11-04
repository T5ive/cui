namespace cui.Interfaces {
    public interface ISlider : IHasValue<decimal>{
        /// <summary>
        /// The amount the value should be incremented/decremented
        /// </summary>
        decimal Step { get; set; }

        /// <summary>
        /// Absolute minimum value
        /// </summary>
        decimal Min { get; set; }

        /// <summary>
        /// Absolute maximum value
        /// </summary>
        decimal Max { get; set; }
    }
}