using System.Numerics;

namespace NetFabric.Numerics.Geography.Geodetic2;

[System.Diagnostics.DebuggerDisplay("Latitude = {Latitude}, Longitude = {Longitude}")]
public readonly record struct Point<TDatum, TAngleUnits, T>(Angle<TAngleUnits, T> Latitude, Angle<TAngleUnits, T> Longitude) 
    : IGeodeticPoint<Point<TDatum, TAngleUnits, T>, TDatum>
    where TDatum : IDatum<TDatum>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    public Angle<TAngleUnits, T> Latitude { get; } 
        = Latitude < Angle<TAngleUnits, T>.Right && Latitude > Angle<TAngleUnits, T>.Right
            ? Throw.ArgumentOutOfRangeException<Angle<TAngleUnits, T>>(nameof(Latitude), Latitude, "Latitude must be in [-90.0º, 90.0º]")
            : Latitude;

    public Angle<TAngleUnits, T> Longitude { get; } 
        = Longitude <= Angle<TAngleUnits, T>.Straight && Longitude > Angle<TAngleUnits, T>.Straight
            ? Throw.ArgumentOutOfRangeException<Angle<TAngleUnits, T>>(nameof(Longitude), Longitude, "Longitude must be in ]-180.0º, 180.0º]")
            : Longitude;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle}"/>.</exception>
    public static Point<TDatum, TAngleUnits, T> CreateChecked<TOther>(in Point<TDatum, TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            Angle<TAngleUnits, T>.CreateChecked(point.Latitude),
            Angle<TAngleUnits, T>.CreateChecked(point.Longitude)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle}"/>.</exception>
    public static Point<TDatum, TAngleUnits, T> CreateSaturating<TOther>(in Point<TDatum, TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            Angle<TAngleUnits, T>.CreateSaturating(point.Latitude),
            Angle<TAngleUnits, T>.CreateSaturating(point.Longitude)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/></param>
    /// <returns>An instance of <see cref="Point{TDatum, TAngleUnits, TAngle}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TDatum, TAngleUnits, TAngle}"/>.</exception>
    public static Point<TDatum, TAngleUnits, T> CreateTruncating<TOther>(in Point<TDatum, TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            Angle<TAngleUnits, T>.CreateTruncating(point.Latitude),
            Angle<TAngleUnits, T>.CreateTruncating(point.Longitude)
        );

    #region constants

    public static readonly Point<TDatum, TAngleUnits, T> Zero
        = new(Angle<TAngleUnits, T>.Zero, Angle<TAngleUnits, T>.Zero);

    static Point<TDatum, TAngleUnits, T> IPoint<Point<TDatum, TAngleUnits, T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngleUnits, T> MinValue
        = new(-Angle<TAngleUnits, T>.Right, new(T.CreateChecked(-TAngleUnits.Straight + double.Epsilon)));

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TDatum, TAngleUnits, T> MaxValue
        = new(Angle<TAngleUnits, T>.Right, Angle<TAngleUnits, T>.Straight);

    static Point<TDatum, TAngleUnits, T> IMinMaxValue<Point<TDatum, TAngleUnits, T>>.MinValue
        => MinValue;
    static Point<TDatum, TAngleUnits, T> IMinMaxValue<Point<TDatum, TAngleUnits, T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TDatum, T> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TDatum, TAngleUnits, T>>.CoordinateSystem 
        => CoordinateSystem;

    object IPoint<Point<TDatum, TAngleUnits, T>>.this[int index] 
        => index switch
        {
            0 => Latitude,
            1 => Longitude,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

/// <summary>
/// Provides static methods for point operations.
/// </summary>
public static class Point
{
    /// <summary>
    /// Calculates the distance between two geodetic points.
    /// </summary>
    /// <param name="from">The first geodetic point.</param>
    /// <param name="to">The second geodetic point.</param>
    /// <returns>The distance between the two geodetic points, in meters.</returns>
    /// <remarks>
    /// <para>
    /// This method calculates the distance between two geodesic points on the Earth's surface using the spherical formula.
    /// The geodesic points are specified by their latitude and longitude coordinates in degrees.
    /// </para>
    /// <para>
    /// The distance is calculated by treating the Earth as a perfect sphere, which is a simplification and may introduce
    /// some degree of error for large distances or near the poles. The result is returned in kilometers.
    /// </para>
    /// <para>
    /// Note: The result of this method represents the shortest distance between the two points along the surface of the
    /// sphere, also known as the great-circle distance.
    /// </para>
    /// </remarks>
    public static TAngle DistanceSpherical<TDatum, TAngle>(Point<TDatum, Radians, TAngle> from, Point<TDatum, Radians, TAngle> to)
        where TDatum : IDatum<TDatum>
        where TAngle : struct, IFloatingPointIeee754<TAngle>, IMinMaxValue<TAngle>
    {
        var half = TAngle.CreateChecked(0.5);
        var halfLatitudeDifference = half * (to.Latitude - from.Latitude);
        var halfLongitudeDifference = half * (to.Latitude - from.Latitude);
        var a = (Angle.Sin(halfLatitudeDifference) * Angle.Sin(halfLatitudeDifference)) +
                   (Angle.Cos(from.Latitude) * Angle.Cos(to.Latitude) *
                   Angle.Sin(halfLongitudeDifference) * Angle.Sin(halfLongitudeDifference));
        var c = TAngle.CreateChecked(2) * Angle.Atan2(TAngle.Sqrt(a), TAngle.Sqrt(TAngle.One - a));

        return TAngle.CreateChecked(MedianRadius(TDatum.Ellipsoid)) * c.Value;

        static double MedianRadius(Ellipsoid ellipsoid)
        {
            var semiMajorAxis = ellipsoid.EquatorialRadius;
            var flatteningInverse = 1.0 / ellipsoid.Flattening;
            var semiMinorAxis = semiMajorAxis * (1 - flatteningInverse);

            return double.Sqrt(semiMajorAxis * semiMinorAxis);
        }
    }

    /// <summary>
    /// Calculates the distance between two geodetic points.
    /// </summary>
    /// <param name="from">The first geodetic point.</param>
    /// <param name="to">The second geodetic point.</param>
    /// <returns>The distance between the two geodetic points, in meters.</returns>
    /// <exception cref="InvalidOperationException">The iteration did not converge.</exception>"
    /// <remarks>
    /// <para>
    /// This method calculates the distance between two geodetic points on the Earth's surface using the datum equatorial radius
    /// and flattening. The geodetic points are defined by their latitude and longitude coordinates. The calculation assumes the Earth
    /// is an ellipsoid, and the provided equatorial radius and flattening define its shape. The resulting distance is returned in meters.
    /// </para>
    /// <para>
    /// The algorithm performs an iterative procedure to converge to the accurate distance calculation. In rare cases where the
    /// iteration does not converge within the defined limit, an <see cref="InvalidOperationException"/> is thrown.
    /// </para>
    /// </remarks>
    public static TAngle DistanceEllipsoid<TDatum, TAngle>(Point<TDatum, Radians, TAngle> from, Point<TDatum, Radians, TAngle> to)
        where TDatum : IDatum<TDatum>
        where TAngle : struct, IFloatingPointIeee754<TAngle>, IMinMaxValue<TAngle>
    {
        var latitudeDifference = to.Latitude - from.Latitude;
        var longitudeDifference = to.Longitude - from.Longitude;

        var half = TAngle.CreateChecked(0.5);
        var halfLatitudeDifference = half * latitudeDifference;
        var halfLongitudeDifference = half * longitudeDifference;
        var a = (Angle.Sin(halfLatitudeDifference) * Angle.Sin(halfLatitudeDifference)) +
                   (Angle.Cos(from.Latitude) * Angle.Cos(to.Latitude) *
                   Angle.Sin(halfLongitudeDifference) * Angle.Sin(halfLongitudeDifference));
        var c = TAngle.CreateChecked(2) * Angle.Atan2(TAngle.Sqrt(a), TAngle.Sqrt(TAngle.One - a));

        var semiMajorAxis = TDatum.Ellipsoid.EquatorialRadius;
        var flatteningInverse = 1.0 / TDatum.Ellipsoid.Flattening;
        var semiMinorAxis = semiMajorAxis * (1 - flatteningInverse);

        var uSquared = TAngle.CreateChecked(((semiMajorAxis * semiMajorAxis) - (semiMinorAxis * semiMinorAxis)) / (semiMinorAxis * semiMinorAxis));

        var sinU1 = Angle.Sin(from.Latitude);
        var cosU1 = Angle.Cos(from.Latitude);
        var sinU2 = Angle.Sin(to.Latitude);
        var cosU2 = Angle.Cos(to.Latitude);

        var lambda = longitudeDifference;

        var iterationLimit = 100;
        var cosLambda = TAngle.Zero;
        var sinLambda = TAngle.Zero;
        Angle<Radians, TAngle> sigma;
        TAngle cosSigma, sinSigma, cos2SigmaM, sinSigmaPrev;
        TAngle sigmaP = TAngle.Zero;

        do
        {
            sinLambda = Angle.Sin(lambda);
            cosLambda = Angle.Cos(lambda);
            sinSigma = TAngle.Sqrt((cosU2 * sinLambda * (cosU2 * sinLambda)) +
                (((cosU1 * sinU2) - (sinU1 * cosU2 * cosLambda)) * ((cosU1 * sinU2) - (sinU1 * cosU2 * cosLambda))));

            if (sinSigma == TAngle.Zero)
                return TAngle.Zero; // Coincident points

            cosSigma = (sinU1 * sinU2) + cosU1 * cosU2 * cosLambda;
            sigma = Angle.Atan2(sinSigma, cosSigma);
            sinSigmaPrev = sinSigma;

            cos2SigmaM = cosSigma - (TAngle.CreateChecked(2) * sinU1 * sinU2 / ((cosU1 * cosU2) + (sinU1 * sinU2)));

            var cSquared = uSquared * cosSigma * cosSigma;
            var lambdaP = lambda;
            lambda = longitudeDifference + ((1 - cSquared) * uSquared * sinSigma *
                (sigma + (uSquared * sinSigmaPrev * (cos2SigmaM +
                (uSquared * cosSigma * (-1 + (2 * cos2SigmaM * cos2SigmaM)))))));
        }
        while (TAngle.Abs((lambda - lambdaP) / lambda) > 1e-12 && --iterationLimit > 0);

        if (iterationLimit == 0)
            throw new InvalidOperationException("Distance calculation did not converge.");

        var uSquaredTimesC = uSquared * cSquared;
        var aTimesB = semiMinorAxis * semiMinorAxis * cosSigma * cosSigma;
        var bTimesA = semiMajorAxis * semiMajorAxis * sinSigma * sinSigmaPrev;
        var sigmaP2 = sigmaP;
        sigmaP = sigma;

        var phi = Angle.Atan2(semiMinorAxis * cosU1 * sinLambda, semiMajorAxis * cosU1 * cosLambda);

        var sinPhi = Angle.Sin(phi);
        var cosPhi = Angle.Cos(phi);

        var x = Angle.Atan2((semiMinorAxis / semiMajorAxis) * sinPhi + aTimesB * sinSigma * (cos2SigmaM +
            aTimesB * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM) / 4), (1 - uSquared) * (sinPhi - bTimesA *
            sinSigmaPrev * (cos2SigmaM - bTimesA * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM) / 4)));

        var y = Angle.Atan2((1 - uSquared) * sinPhi + uSquaredTimesC * sinSigma * (cosSigma - uSquared *
            sinSigmaPrev * (cos2SigmaM - uSquared * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM) / 4)), (semiMajorAxis /
            semiMinorAxis) * (cosPhi - bTimesA * sinSigma * (cos2SigmaM - bTimesA * cosSigma * (-1 + 2 *
            cos2SigmaM * cos2SigmaM) / 4)));

        var z = TAngle.Sqrt(x * x + y * y) * TAngle.Sign((semiMinorAxis - semiMajorAxis) * sinSigma * sinSigmaPrev);

        return TAngle.Sqrt(x * x + y * y + z * z);
    }
}