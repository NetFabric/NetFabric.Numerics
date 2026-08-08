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
    public static Ellipsoid<T> CreateChecked<TOther>(ref readonly Ellipsoid<TOther> ellipsoid)
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
    public static Ellipsoid<T> CreateSaturating<TOther>(ref readonly Ellipsoid<TOther> ellipsoid)
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
    public static Ellipsoid<T> CreateTruncating<TOther>(ref readonly Ellipsoid<TOther> ellipsoid)
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
    /// <typeparam name="T">The floating-point type of the ellipsoid parameters.</typeparam>
    /// <param name="ellipsoid">The reference ellipsoid.</param>
    /// <returns>The surface area of the ellipsoid in the same units as the equatorial radius squared.</returns>
    /// <remarks>
    /// Uses the formula S = 2π a² (1 + ((1-e²)/e) atanh(e)).
    /// When e ≈ 0 (sphere), the formula degenerates to 4π a².
    /// </remarks>
    public static T SurfaceArea<T>(ref readonly Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>, IPowerFunctions<T>, IRootFunctions<T>, ILogarithmicFunctions<T>
    {
        var e2 = EccentricitySquared(in ellipsoid);
        var a = ellipsoid.EquatorialRadius;
        var two = T.CreateChecked(2);
        var aSq = T.Pow(a, two);

        // Handle sphere limit: when e^2 = 0, surface area = 4 π a²
        if (e2 == T.Zero)
            return T.CreateChecked(4) * T.Pi * aSq;

        var e = T.Sqrt(e2);
        // atanh(e) = 0.5 * ln((1+e)/(1-e))
        var atanhe = T.CreateChecked(0.5) * T.Log((T.One + e) / (T.One - e));
        return two * T.Pi * aSq * (T.One + ((T.One - e2) / e) * atanhe);
    }

    /// <summary>
    /// Calculates the volume of the ellipsoid.
    /// </summary>
    /// <value>The volume of the ellipsoid.</value>
    public static T Volume<T>(ref readonly Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>, IPowerFunctions<T>
        => T.CreateChecked(4) * T.Pi * T.Pow(ellipsoid.EquatorialRadius, T.CreateChecked(2)) * PolarRadius(in ellipsoid) / T.CreateChecked(3);

    /// <summary>
    /// Calculates the radius of curvatures at the poles.
    /// </summary>
    /// <value>The radius of curvatures at the poles.</value>
    public static T RadiusOfCurvatureAtPoles<T>(ref readonly Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>, IRootFunctions<T>
        => ellipsoid.EquatorialRadius / T.Sqrt(T.One - EccentricitySquared(in ellipsoid));

    /// <summary>
    /// Calculates the radius of curvature in a meridian plane at the equator.
    /// </summary>
    /// <value>The radius of curvature in a meridian plane at the equator.</value>
    public static T RadiusOfCurvatureAtEquator<T>(ref readonly Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>
        => ellipsoid.EquatorialRadius * (T.One - EccentricitySquared(in ellipsoid));

    /// <summary>
    /// Calculates the semi-minor axis (polar radius) of the ellipsoid.
    /// </summary>
    /// <typeparam name="T">The floating-point type of the ellipsoid parameters.</typeparam>
    /// <param name="ellipsoid">The reference ellipsoid.</param>
    /// <returns>The polar radius b = a(1-f).</returns>
    public static T PolarRadius<T>(ref readonly Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>
        => ellipsoid.EquatorialRadius * (T.One - ellipsoid.Flattening);

    /// <summary>
    /// Calculates the first eccentricity of the ellipsoid.
    /// </summary>
    /// <typeparam name="T">The floating-point type of the ellipsoid parameters.</typeparam>
    /// <param name="ellipsoid">The reference ellipsoid.</param>
    /// <returns>The first eccentricity e = sqrt(f(2-f)).</returns>
    public static T Eccentricity<T>(ref readonly Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>, IRootFunctions<T>
        => T.Sqrt(EccentricitySquared(in ellipsoid));

    /// <summary>
    /// Calculates the first eccentricity squared of the ellipsoid.
    /// </summary>
    /// <typeparam name="T">The floating-point type of the ellipsoid parameters.</typeparam>
    /// <param name="ellipsoid">The reference ellipsoid.</param>
    /// <returns>The first eccentricity squared e² = f(2-f).</returns>
    public static T EccentricitySquared<T>(ref readonly Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>
        => ellipsoid.Flattening * (T.CreateChecked(2) - ellipsoid.Flattening);

    /// <summary>
    /// Calculates the second eccentricity squared of the ellipsoid.
    /// </summary>
    /// <typeparam name="T">The floating-point type of the ellipsoid parameters.</typeparam>
    /// <param name="ellipsoid">The reference ellipsoid.</param>
    /// <returns>The second eccentricity squared e'² = e² / (1 - e²).</returns>
    public static T SecondEccentricitySquared<T>(ref readonly Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>
    {
        var e2 = EccentricitySquared(in ellipsoid);
        return e2 / (T.One - e2);
    }

    /// <summary>
    /// Calculates the arithmetic mean radius of the ellipsoid.
    /// </summary>
    /// <typeparam name="T">The floating-point type of the ellipsoid parameters.</typeparam>
    /// <param name="ellipsoid">The reference ellipsoid.</param>
    /// <returns>The arithmetic mean radius R₁ = (2a + b) / 3.</returns>
    public static T ArithmeticMeanRadius<T>(ref readonly Ellipsoid<T> ellipsoid)
        where T : struct, IFloatingPoint<T>
        => ((T.CreateChecked(2) * ellipsoid.EquatorialRadius) + PolarRadius(in ellipsoid)) / T.CreateChecked(3);

    /// <summary>
    /// Calculates the radius of curvature in the meridian at a given geodetic latitude.
    /// </summary>
    /// <typeparam name="T">The floating-point type of the ellipsoid parameters.</typeparam>
    /// <param name="ellipsoid">The reference ellipsoid.</param>
    /// <param name="latitudeInRadians">The geodetic latitude in radians.</param>
    /// <returns>The meridian radius of curvature M(φ) = a(1-e²) / (1 - e² sin²(φ))^(3/2).</returns>
    public static T RadiusOfCurvatureInMeridian<T>(ref readonly Ellipsoid<T> ellipsoid, T latitudeInRadians)
        where T : struct, IFloatingPoint<T>, IRootFunctions<T>, ITrigonometricFunctions<T>
    {
        var e2 = EccentricitySquared(in ellipsoid);
        var sinPhi = T.Sin(latitudeInRadians);
        var w2 = T.One - e2 * sinPhi * sinPhi;
        return ellipsoid.EquatorialRadius * (T.One - e2) / (T.Sqrt(w2) * w2);
    }

    /// <summary>
    /// Calculates the radius of curvature in the prime vertical at a given geodetic latitude.
    /// </summary>
    /// <typeparam name="T">The floating-point type of the ellipsoid parameters.</typeparam>
    /// <param name="ellipsoid">The reference ellipsoid.</param>
    /// <param name="latitudeInRadians">The geodetic latitude in radians.</param>
    /// <returns>The prime-vertical radius of curvature N(φ) = a / sqrt(1 - e² sin²(φ)).</returns>
    public static T RadiusOfCurvatureInPrimeVertical<T>(ref readonly Ellipsoid<T> ellipsoid, T latitudeInRadians)
        where T : struct, IFloatingPoint<T>, IRootFunctions<T>, ITrigonometricFunctions<T>
    {
        var e2 = EccentricitySquared(in ellipsoid);
        var sinPhi = T.Sin(latitudeInRadians);
        return ellipsoid.EquatorialRadius / T.Sqrt(T.One - e2 * sinPhi * sinPhi);
    }
}