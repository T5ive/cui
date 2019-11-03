using System;

namespace cui.Interfaces {
    public interface ISlider<T> : IHasValue<T> where T : IEquatable<T> {
        /// <summary>
        /// The amount the value should be incremented/decremented
        /// </summary>
        T Step { get; set; }

        /// <summary>
        /// Absolute minimum value
        /// </summary>
        T Min { get; set; }

        /// <summary>
        /// Absolute maximum value
        /// </summary>
        T Max { get; set; }
    }
}