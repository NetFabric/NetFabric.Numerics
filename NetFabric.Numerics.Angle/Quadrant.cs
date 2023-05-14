using System;

namespace NetFabric.Numerics
{
    /// <summary>
    /// The four regions divided by the x and y axis.
    /// </summary>
    public enum Quadrant
    {
        /// <summary>
        /// Lies on the positive x axis.
        /// </summary>
        PositiveX,
        /// <summary>
        /// The region where x and y are positive.
        /// </summary>
        First,
        /// <summary>
        /// Lies on the positive y axis.
        /// </summary>
        PositiveY,
        /// <summary>
        /// The region where x is negative and y is positive.
        /// </summary>
        Second,
        /// <summary>
        /// Lies on the negative x axis.
        /// </summary>
        NegativeX,
        /// <summary>
        /// The region where x and y are negative.
        /// </summary>
        Third,
        /// <summary>
        /// Lies on the negative y axis.
        /// </summary>
        NegativeY,
        /// <summary>
        /// The region where x is positive and y is negative.
        /// </summary>
        Fourth
    }
}