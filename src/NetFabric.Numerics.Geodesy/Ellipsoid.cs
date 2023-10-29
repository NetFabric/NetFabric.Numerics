using System.Numerics;

namespace NetFabric.Numerics.Geodesy;

/// <summary>
/// Summary description for Ellipsoid.
/// </summary>
public readonly record struct Ellipsoid<T>(T EquatorialRadius, T Flattening)
    where T : struct, IFloatingPoint<T>
{
    public static readonly Ellipsoid<T> Airy1830 = new(T.CreateChecked(6377563.396), T.One / T.CreateChecked(299.3249646));
    public static readonly Ellipsoid<T> Australian1965 = new(T.CreateChecked(6378160.0), T.One / T.CreateChecked(298.25));
    public static readonly Ellipsoid<T> Bessel1841 = new(T.CreateChecked(6377397.155), T.One / T.CreateChecked(299.1528128));
    public static readonly Ellipsoid<T> Clarke1880 = new(T.CreateChecked(6378249.145), T.One / T.CreateChecked(293.465));
    public static readonly Ellipsoid<T> Clarke1866 = new(T.CreateChecked(6378206.4), T.One / T.CreateChecked(294.9786982));
    public static readonly Ellipsoid<T> Everest1830 = new(T.CreateChecked(6377276.345), T.One / T.CreateChecked(300.8017));
    public static readonly Ellipsoid<T> Fischer1960 = new(T.CreateChecked(6378166.0), T.One / T.CreateChecked(298.3));
    public static readonly Ellipsoid<T> Fischer1968 = new(T.CreateChecked(6378150.0), T.One / T.CreateChecked(298.3));
    public static readonly Ellipsoid<T> Grs1967 = new(T.CreateChecked(6378160.0), T.One / T.CreateChecked(298.247167427));
    public static readonly Ellipsoid<T> Grs1975 = new(T.CreateChecked(6378140.0), T.One / T.CreateChecked(298.257));
    public static readonly Ellipsoid<T> Grs1980 = new(T.CreateChecked(6378137.0), T.One / T.CreateChecked(298.257222101));
    public static readonly Ellipsoid<T> Hayford1924 = new(T.CreateChecked(6378388.0), T.One / T.CreateChecked(297.0));
    public static readonly Ellipsoid<T> Helmert1906 = new(T.CreateChecked(6378200.0), T.One / T.CreateChecked(298.3));
    public static readonly Ellipsoid<T> Hough1956 = new(T.CreateChecked(6378270.0), T.One / T.CreateChecked(297.0));
    public static readonly Ellipsoid<T> International1924 = new(T.CreateChecked(6378388.0), T.One / T.CreateChecked(297.0));
    public static readonly Ellipsoid<T> Krassovsky1940 = new(T.CreateChecked(6378245.0), T.One / T.CreateChecked(298.3));
    public static readonly Ellipsoid<T> SouthAmerican1969 = new(T.CreateChecked(6378160.0), T.One / T.CreateChecked(298.25));
    public static readonly Ellipsoid<T> Wgs1960 = new(T.CreateChecked(6378165.0), T.One / T.CreateChecked(298.3));
    public static readonly Ellipsoid<T> WGS1966 = new(T.CreateChecked(6378145.0), T.One / T.CreateChecked(298.25));
    public static readonly Ellipsoid<T> WGS1972 = new(T.CreateChecked(6378135.0), T.One / T.CreateChecked(298.26));
    public static readonly Ellipsoid<T> WGS1984 = new(T.CreateChecked(6378137.0), T.One / T.CreateChecked(298.257223563));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="ellipsoid"/>.</typeparam>
    /// <param name="ellipsoid">The value which is used to create the instance of <see cref="Ellipsoid{T}"/></param>
    /// <returns>An instance of <see cref="Ellipsoid{T}"/> created from <paramref name="ellipsoid" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="ellipsoid" /> is not representable by <see cref="Ellipsoid{T}"/>.</exception>
    public static Ellipsoid<T> CreateChecked<TOther>(in Ellipsoid<TOther> ellipsoid)
        where TOther : struct, IFloatingPoint<TOther>
        => new(
            T.CreateChecked(ellipsoid.EquatorialRadius),
            T.CreateChecked(ellipsoid.Flattening)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="ellipsoid"/>.</typeparam>
    /// <param name="ellipsoid">The value which is used to create the instance of <see cref="Ellipsoid{T}"/></param>
    /// <returns>An instance of <see cref="Ellipsoid{T}"/> created from <paramref name="ellipsoid" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="ellipsoid" /> is not representable by <see cref="Ellipsoid{T}"/>.</exception>
    public static Ellipsoid<T> CreateSaturating<TOther>(in Ellipsoid<TOther> ellipsoid)
        where TOther : struct, IFloatingPoint<TOther>
        => new(
            T.CreateSaturating(ellipsoid.EquatorialRadius),
            T.CreateSaturating(ellipsoid.Flattening)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="ellipsoid"/>.</typeparam>
    /// <param name="ellipsoid">The value which is used to create the instance of <see cref="Ellipsoid{T}"/></param>
    /// <returns>An instance of <see cref="Ellipsoid{T}"/> created from <paramref name="ellipsoid" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="ellipsoid" /> is not representable by <see cref="Ellipsoid{T}"/>.</exception>
    public static Ellipsoid<T> CreateTruncating<TOther>(in Ellipsoid<TOther> ellipsoid)
        where TOther : struct, IFloatingPoint<TOther>
        => new(
            T.CreateTruncating(ellipsoid.EquatorialRadius),
            T.CreateTruncating(ellipsoid.Flattening)
        );
}

/// <summary>
/// Provides static methods for ellipsoid operations.
/// </summary>
public static class Ellipsoid
{
    /// <summary>
    /// Calculates the surface area of the ellipsoid.
    /// </summary>
    /// <value>The surface area of the ellipsoid.</value>
    public static T SurfaceArea<T>(in Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>, IPowerFunctions<T>, IRootFunctions<T>, ILogarithmicFunctions<T>
    {
        var eccentricity = Eccentricity(ellipsoid);
        var two = T.One + T.One;
        return (two * T.Pi * T.Pow(ellipsoid.EquatorialRadius, two)) + (T.Pi * (T.Pow(PolarRadius(ellipsoid), two) / eccentricity) * T.Log10((T.One + eccentricity) / (T.One - eccentricity)));
    }

    /// <summary>
    /// Calculates the volume of the ellipsoid.
    /// </summary>
    /// <value>The volume of the ellipsoid.</value>
    public static T Volume<T>(in Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>, IPowerFunctions<T>
        => T.CreateChecked(4) * T.Pi * T.Pow(ellipsoid.EquatorialRadius, T.CreateChecked(2)) * PolarRadius(ellipsoid) / T.CreateChecked(3);

    /// <summary>
    /// Calculates the radius of curvatures at the poles.
    /// </summary>
    /// <value>The radius of curvatures at the poles.</value>
    public static T RadiusOfCurvatureAtPoles<T>(in Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>, IRootFunctions<T>
        => ellipsoid.EquatorialRadius / T.Sqrt(T.One - EccentricitySquared(ellipsoid));

    /// <summary>
    /// Calculates the radius of curvature in a meridian plane at the equator.
    /// </summary>
    /// <value>The radius of curvature in a meridian plane at the equator.</value>
    public static T RadiusOfCurvatureAtEquator<T>(in Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>
        => ellipsoid.EquatorialRadius * (T.One - EccentricitySquared(ellipsoid));

    public static T PolarRadius<T>(in Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>
        => ellipsoid.EquatorialRadius * (T.One - ellipsoid.Flattening);

    public static T Eccentricity<T>(in Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>, IRootFunctions<T>
        => T.Sqrt(EccentricitySquared(ellipsoid));

    public static T EccentricitySquared<T>(in Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>
        => ellipsoid.Flattening * (T.CreateChecked(2) - ellipsoid.Flattening);

    public static T ArithmeticMeanRadius<T>(in Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>
        => ((T.CreateChecked(2) * ellipsoid.EquatorialRadius) + PolarRadius(ellipsoid)) / T.CreateChecked(3);
}
