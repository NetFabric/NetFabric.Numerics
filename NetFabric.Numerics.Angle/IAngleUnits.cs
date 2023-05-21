using System;

namespace NetFabric.Numerics;

public interface IAngleUnits<TSelf>
    where TSelf: IAngleUnits<TSelf>
{
    static abstract string Symbol { get; }

    static abstract double Zero { get; }
    static abstract double Right { get; }
    static abstract double Straight { get; }
    static abstract double Full { get; }
}

public class Degrees
    : IAngleUnits<Degrees>
{
    public static string Symbol => "º";

    public static double Zero => 0.0;
    public static double Right => 90.0;
    public static double Straight => 180.0;
    public static double Full => 360.0;
}

public class Radians
    : IAngleUnits<Radians>
{
    public static string Symbol => " rad";

    public static double Zero => 0.0;
    public static double Right => Math.PI / 2.0;
    public static double Straight => Math.PI;
    public static double Full => Math.PI * 2.0;
}

public class Gradians
    : IAngleUnits<Gradians>
{
    public static string Symbol => " grad";

    public static double Zero => 0.0;
    public static double Right => 100.0;
    public static double Straight => 200.0;
    public static double Full => 400.0;
}

public class Revolutions
    : IAngleUnits<Revolutions>
{
    public static string Symbol => " rev";

    public static double Zero => 0.0;
    public static double Right => 0.25;
    public static double Straight => 0.5;
    public static double Full => 1.0;
}
