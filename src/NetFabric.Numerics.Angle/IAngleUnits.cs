namespace NetFabric.Numerics
{
    /// <summary>
    /// Defines the units of an angle.
    /// </summary>
    /// <typeparam name="TSelf">The type that implements the interface.</typeparam>
    public interface IAngleUnits<TSelf>
        where TSelf : IAngleUnits<TSelf>
    {
        /// <summary>
        /// Gets the symbol of the units.
        /// </summary>
        /// <value>The symbol representing the angle units.</value>
        static abstract string Symbol { get; }

        /// <summary>
        /// Gets the value of a zero angle.
        /// </summary>
        /// <value>The value of a zero angle in these units.</value>
        static abstract double Zero { get; }

        /// <summary>
        /// Gets the value of a right angle.
        /// </summary>
        /// <value>The value of a right angle in these units.</value>
        static abstract double Right { get; }

        /// <summary>
        /// Gets the value of a straight angle.
        /// </summary>
        /// <value>The value of a straight angle in these units.</value>
        static abstract double Straight { get; }

        /// <summary>
        /// Gets the value of a full angle.
        /// </summary>
        /// <value>The value of a full angle in these units.</value>
        static abstract double Full { get; }
    }

    /// <summary>
    /// Defines the units of an angle in degrees.
    /// </summary>
    public struct Degrees
        : IAngleUnits<Degrees>
    {
        /// <summary>
        /// Gets the symbol of the units.
        /// </summary>
        /// <value>The symbol for degrees (º).</value>
        public static string Symbol => "º";

        /// <summary>
        /// Gets the value in degrees of a zero angle.
        /// </summary>
        /// <value>The value of a zero angle in degrees (0.0).</value>
        public static double Zero => 0.0;

        /// <summary>
        /// Gets the value in degrees of a right angle.
        /// </summary>
        /// <value>The value of a right angle in degrees (90.0).</value>
        public static double Right => 90.0;

        /// <summary>
        /// Gets the value in degrees of a straight angle.
        /// </summary>
        /// <value>The value of a straight angle in degrees (180.0).</value>
        public static double Straight => 180.0;

        /// <summary>
        /// Gets the value in degrees of a full angle.
        /// </summary>
        /// <value>The value of a full angle in degrees (360.0).</value>
        public static double Full => 360.0;
    }

    /// <summary>
    /// Defines the units of an angle in radians.
    /// </summary>
    public struct Radians
        : IAngleUnits<Radians>
    {
        /// <summary>
        /// Gets the symbol of the units.
        /// </summary>
        /// <value>The symbol for radians (rad).</value>
        public static string Symbol => "rad";

        /// <summary>
        /// Gets the value in radians of a zero angle.
        /// </summary>
        /// <value>The value of a zero angle in radians (0.0).</value>
        public static double Zero => 0.0;

        /// <summary>
        /// Gets the value in radians of a right angle.
        /// </summary>
        /// <value>The value of a right angle in radians (π/2.0).</value>
        public static double Right => Math.PI / 2.0;

        /// <summary>
        /// Gets the value in radians of a straight angle.
        /// </summary>
        /// <value>The value of a straight angle in radians (π).</value>
        public static double Straight => Math.PI;

        /// <summary>
        /// Gets the value in radians of a full angle.
        /// </summary>
        /// <value>The value of a full angle in radians (2π).</value>
        public static double Full => Math.PI * 2.0;
    }

    /// <summary>
    /// Defines the units of an angle in gradians.
    /// </summary>
    public struct Gradians
        : IAngleUnits<Gradians>
    {
        /// <summary>
        /// Gets the symbol of the units.
        /// </summary>
        /// <value>The symbol for gradians (grad).</value>
        public static string Symbol => "grad";

        /// <summary>
        /// Gets the value in gradians of a zero angle.
        /// </summary>
        /// <value>The value of a zero angle in gradians (0.0).</value>
        public static double Zero => 0.0;

        /// <summary>
        /// Gets the value in gradians of a right angle.
        /// </summary>
        /// <value>The value of a right angle in gradians (100.0).</value>
        public static double Right => 100.0;

        /// <summary>
        /// Gets the value in gradians of a straight angle.
        /// </summary>
        /// <value>The value of a straight angle in gradians (200.0).</value>
        public static double Straight => 200.0;

        /// <summary>
        /// Gets the value in gradians of a full angle.
        /// </summary>
        /// <value>The value of a full angle in gradians (400.0).</value>
        public static double Full => 400.0;
    }

    /// <summary>
    /// Defines the units of an angle in revolutions.
    /// </summary>
    public struct Revolutions
        : IAngleUnits<Revolutions>
    {
        /// <summary>
        /// Gets the symbol of the units.
        /// </summary>
        /// <value>The symbol for revolutions (rev).</value>
        public static string Symbol => "rev";

        /// <summary>
        /// Gets the value in revolutions of a zero angle.
        /// </summary>
        /// <value>The value of a zero angle in revolutions (0.0).</value>
        public static double Zero => 0.0;

        /// <summary>
        /// Gets the value in revolutions of a right angle.
        /// </summary>
        /// <value>The value of a right angle in revolutions (0.25).</value>
        public static double Right => 0.25;

        /// <summary>
        /// Gets the value in revolutions of a straight angle.
        /// </summary>
        /// <value>The value of a straight angle in revolutions (0.5).</value>
        public static double Straight => 0.5;

        /// <summary>
        /// Gets the value in revolutions of a full angle.
        /// </summary>
        /// <value>The value of a full angle in revolutions (1.0).</value>
        public static double Full => 1.0;
    }
}
