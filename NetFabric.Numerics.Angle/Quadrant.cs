using System;

namespace NetFabric.Numerics
{
    /// <summary>
    /// The four regions divided by the x and y axis.
    /// </summary>
    public enum Quadrant
    {
        /// <summary>
        /// The region where x and y are positive.
        /// </summary>
        First,
        /// <summary>
        /// The region where x is negative and y is positive.
        /// </summary>
        Second,
        /// <summary>
        /// The region where x and y are negative.
        /// </summary>
        Third,
        /// <summary>
        /// The region where x is positive and y is negative.
        /// </summary>
        Fourth
    }
}