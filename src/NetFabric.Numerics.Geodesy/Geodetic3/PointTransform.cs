using System.Numerics;

namespace NetFabric.Numerics.Geodesy.Geodetic3;

/// <summary>
/// Provides static helpers for converting <see cref="Point{TDatum,TAngleUnits,T}"/> to and from
/// geocentric ECEF (Earth-Centred Earth-Fixed) Cartesian coordinates, and for performing
/// 7-parameter Helmert datum transformations between geodetic reference frames.
/// </summary>
/// <remarks>
/// <para>
/// <b>Offset direction convention:</b> each datum's <see cref="IDatum{T}.Offset"/> stores the
/// forward (datum → WGS84) transform parameters, following EPSG conventions.
/// <see cref="TransformDatum{TDatumFrom,TDatumTo,TAngleUnits,T}"/> applies
/// <c>TDatumFrom.Offset</c> in the forward direction and <c>TDatumTo.Offset</c> in the inverse
/// direction, so either or both datums can be WGS84 (whose offset is zero).
/// </para>
/// <para>
/// <b>Rotation sign convention:</b> coordinate-frame / Burša–Wolf / IERS.  A positive RZ
/// rotation maps the X-axis toward the positive Y-axis.  If a datum's published parameters
/// follow the position-vector (Helmert) convention, negate RX, RY, and RZ before storing them
/// in <see cref="Offset{T}"/>.
/// </para>
/// <para>
/// TODO (Stage 5): When the Geodetic ↔ ECEF stage is implemented, consolidate the
/// <see cref="ToEcef{TDatum,TAngleUnits,T}"/> and <see cref="FromEcef{TDatum,TAngleUnits,T}"/>
/// helpers into the shared conversion layer and redirect these methods.
/// </para>
/// </remarks>
public static class Point
{
    /// <summary>
    /// Converts a geodetic 3-D point to geocentric ECEF Cartesian coordinates using the
    /// ellipsoid defined by <typeparamref name="TDatum"/>.
    /// </summary>
    /// <typeparam name="TDatum">
    /// The geodetic datum that provides the reference ellipsoid.  The datum's
    /// <see cref="IDatum{T}.Ellipsoid"/> is used for the conversion.
    /// </typeparam>
    /// <typeparam name="TAngleUnits">
    /// The angular unit system in which latitude and longitude are expressed
    /// (e.g. <see cref="Degrees"/> or <see cref="Radians"/>).
    /// </typeparam>
    /// <typeparam name="T">The floating-point scalar type.</typeparam>
    /// <param name="point">The geodetic point to convert.</param>
    /// <returns>
    /// A <see cref="Rectangular3D.Point{T}"/> whose X, Y, Z components are in the same linear
    /// units as <see cref="Ellipsoid{T}.EquatorialRadius"/> (typically metres).
    /// </returns>
    public static Rectangular3D.Point<T> ToEcef<TDatum, TAngleUnits, T>(
        Point<TDatum, TAngleUnits, T> point)
        where TDatum : IDatum<T>
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
    {
        var ellipsoid = TDatum.Ellipsoid;

        // Convert angular values to radians.
        var radiansPerUnit = T.CreateChecked(double.Pi / TAngleUnits.Straight);
        var latRad = point.Latitude.Value * radiansPerUnit;
        var lonRad = point.Longitude.Value * radiansPerUnit;
        var h = point.Height;

        // Ellipsoid parameters.
        var a = ellipsoid.EquatorialRadius;
        var e2 = Ellipsoid.EccentricitySquared(in ellipsoid);

        // Prime-vertical radius of curvature N(φ).
        var sinLat = T.Sin(latRad);
        var cosLat = T.Cos(latRad);
        var sinLon = T.Sin(lonRad);
        var cosLon = T.Cos(lonRad);

        var n = a / T.Sqrt(T.One - e2 * sinLat * sinLat);

        var x = (n + h) * cosLat * cosLon;
        var y = (n + h) * cosLat * sinLon;
        var z = (n * (T.One - e2) + h) * sinLat;

        return new(x, y, z);
    }

    /// <summary>
    /// Converts geocentric ECEF Cartesian coordinates back to a geodetic 3-D point using the
    /// ellipsoid defined by <typeparamref name="TDatum"/>.
    /// </summary>
    /// <typeparam name="TDatum">
    /// The geodetic datum that provides the reference ellipsoid.  The datum's
    /// <see cref="IDatum{T}.Ellipsoid"/> is used for the conversion.
    /// </typeparam>
    /// <typeparam name="TAngleUnits">
    /// The angular unit system for the returned latitude and longitude.
    /// </typeparam>
    /// <typeparam name="T">The floating-point scalar type.</typeparam>
    /// <param name="x">ECEF X coordinate (metres).</param>
    /// <param name="y">ECEF Y coordinate (metres).</param>
    /// <param name="z">ECEF Z coordinate (metres).</param>
    /// <returns>
    /// A <see cref="Point{TDatum,TAngleUnits,T}"/> with latitude in [−90°, +90°] and longitude
    /// in (−180°, +180°].  Exactly −180° is normalized to +180°.
    /// </returns>
    public static Point<TDatum, TAngleUnits, T> FromEcef<TDatum, TAngleUnits, T>(T x, T y, T z)
        where TDatum : IDatum<T>
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
    {
        var ellipsoid = TDatum.Ellipsoid;

        var a = ellipsoid.EquatorialRadius;
        var e2 = Ellipsoid.EccentricitySquared(in ellipsoid);

        // Longitude (exact).
        var lonRad = T.Atan2(y, x);

        // Bowring's iterative method for latitude and height.
        var p = T.Sqrt(x * x + y * y);
        var b = Ellipsoid.PolarRadius(in ellipsoid);
        var ep2 = Ellipsoid.SecondEccentricitySquared(in ellipsoid);

        // Initial estimate (Bowring 1985).
        var theta = T.Atan2(z * a, p * b);
        var sinTheta = T.Sin(theta);
        var cosTheta = T.Cos(theta);

        var latRad = T.Atan2(
            z + ep2 * b * sinTheta * sinTheta * sinTheta,
            p - e2 * a * cosTheta * cosTheta * cosTheta);

        // Iterate until convergence (typically 2-3 iterations).
        for (var i = 0; i < 10; i++)
        {
            var prevLat = latRad;
            var sinPhi = T.Sin(latRad);
            var n = a / T.Sqrt(T.One - e2 * sinPhi * sinPhi);
            latRad = T.Atan2(z + e2 * n * sinPhi, p);
            if (T.Abs(latRad - prevLat) < T.CreateChecked(1e-12))
                break;
        }

        // Height.
        var sinLatFinal = T.Sin(latRad);
        var cosLatFinal = T.Cos(latRad);
        var nFinal = a / T.Sqrt(T.One - e2 * sinLatFinal * sinLatFinal);
        T h;
        if (T.Abs(cosLatFinal) > T.CreateChecked(1e-10))
            h = p / cosLatFinal - nFinal;
        else
            h = T.Abs(z) / T.Abs(sinLatFinal) - nFinal * (T.One - e2);

        // Convert radians back to the requested angle units.
        var unitsPerRadian = T.CreateChecked(TAngleUnits.Straight / double.Pi);
        var latInUnits = latRad * unitsPerRadian;
        var lonInUnits = lonRad * unitsPerRadian;

        // Normalize exactly −180° to +180° so the result is always in (−Straight, +Straight].
        var straight = T.CreateChecked(TAngleUnits.Straight);
        if (lonInUnits == -straight)
            lonInUnits = straight;

        return new(new Angle<TAngleUnits, T>(latInUnits), new Angle<TAngleUnits, T>(lonInUnits), h);
    }

    /// <summary>
    /// Transforms a geodetic 3-D point from <typeparamref name="TDatumFrom"/> to
    /// <typeparamref name="TDatumTo"/> using the 7-parameter linearized Helmert transformation.
    /// </summary>
    /// <remarks>
    /// The transformation pipeline is:
    /// <list type="number">
    ///   <item>Convert the source geodetic point to ECEF using <typeparamref name="TDatumFrom"/>'s ellipsoid.</item>
    ///   <item>
    ///     Apply the Helmert transformation using the datum offsets:
    ///     <list type="bullet">
    ///       <item><see cref="Offset{T}.XYZOffset"/> — translation vector (ΔX, ΔY, ΔZ) in metres.</item>
    ///       <item><see cref="Offset{T}.RX"/>, <see cref="Offset{T}.RY"/>, <see cref="Offset{T}.RZ"/> —
    ///         small rotations in arc-seconds (coordinate-frame / Burša–Wolf convention:
    ///         a positive RZ rotation maps the X-axis toward the positive Y-axis).</item>
    ///       <item><see cref="Offset{T}.SC"/> — scale correction in parts-per-million (ppm);
    ///         applied as <c>1 + SC × 10⁻⁶</c>.</item>
    ///     </list>
    ///   </item>
    ///   <item>Convert the transformed ECEF point back to geodetic using <typeparamref name="TDatumTo"/>'s ellipsoid.</item>
    /// </list>
    /// <para>
    /// <b>Offset direction convention:</b> each datum's <see cref="IDatum{T}.Offset"/> encodes
    /// the forward (datum → WGS84) transform, matching EPSG-published parameters.
    /// To go WGS84 → target datum the inverse of TDatumTo's offset is applied.
    /// </para>
    /// <para>
    /// <b>Rotation sign convention:</b> coordinate-frame (Burša–Wolf / IERS).
    /// If a datum's parameters follow the position-vector convention, negate RX, RY, and RZ
    /// before storing them in <see cref="Offset{T}"/>.
    /// </para>
    /// </remarks>
    /// <typeparam name="TDatumFrom">Source datum; its <see cref="IDatum{T}.Offset"/> is applied forward.</typeparam>
    /// <typeparam name="TDatumTo">Target datum; its <see cref="IDatum{T}.Offset"/> is applied inverse.</typeparam>
    /// <typeparam name="TAngleUnits">Angular unit system shared by source and result.</typeparam>
    /// <typeparam name="T">Floating-point scalar type.</typeparam>
    /// <param name="source">The point to transform, expressed in <typeparamref name="TDatumFrom"/>.</param>
    /// <returns>
    /// A geodetic 3-D point in <typeparamref name="TDatumTo"/> with latitude in [−90°, +90°]
    /// and longitude in (−180°, +180°].
    /// </returns>
    public static Point<TDatumTo, TAngleUnits, T> TransformDatum<TDatumFrom, TDatumTo, TAngleUnits, T>(
        Point<TDatumFrom, TAngleUnits, T> source)
        where TDatumFrom : IDatum<T>
        where TDatumTo : IDatum<T>
        where TAngleUnits : IAngleUnits
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
    {
        // Step 1: geodetic -> ECEF using source datum ellipsoid.
        var ecef = ToEcef<TDatumFrom, TAngleUnits, T>(source);

        var x = ecef.X;
        var y = ecef.Y;
        var z = ecef.Z;

        // Step 2a: apply TDatumFrom.Offset forward (datum → WGS84 pivot frame).
        // Each datum's Offset encodes the datum → WGS84 direction per EPSG convention.
        // For TDatumFrom = WGS84 (Offset = Zero) this is a no-op.
        (x, y, z) = ApplyHelmert(x, y, z, TDatumFrom.Offset, inverse: false);

        // Step 2b: apply the inverse of TDatumTo.Offset (WGS84 pivot frame → target datum).
        // Inverting the datum → WGS84 offset gives the WGS84 → datum direction.
        // For TDatumTo = WGS84 (Offset = Zero) this is a no-op.
        (x, y, z) = ApplyHelmert(x, y, z, TDatumTo.Offset, inverse: true);

        // Step 3: ECEF -> geodetic using target datum ellipsoid.
        return FromEcef<TDatumTo, TAngleUnits, T>(x, y, z);
    }

    /// <summary>
    /// Applies (or inverts) a linearized 7-parameter Burša–Wolf / coordinate-frame Helmert
    /// transformation to a single ECEF point.
    /// </summary>
    /// <remarks>
    /// Forward form (datum → WGS84):
    /// <code>
    ///   X' = ΔX + s·( X + rz·Y − ry·Z)
    ///   Y' = ΔY + s·(−rz·X + Y + rx·Z)
    ///   Z' = ΔZ + s·( ry·X − rx·Y + Z)
    /// </code>
    /// where s = 1 + SC·10⁻⁶, RX/RY/RZ are converted from arc-seconds to radians.
    /// The inverse negates ΔX/ΔY/ΔZ, RX/RY/RZ, and SC before applying the same formula
    /// (valid for the small-angle, small-scale approximation).
    /// </remarks>
    private static (T x, T y, T z) ApplyHelmert<T>(T x, T y, T z, Offset<T> offset, bool inverse)
        where T : struct, IFloatingPointIeee754<T>, IMinMaxValue<T>
    {
        var arcSecToRad = T.CreateChecked(double.Pi / (180.0 * 3600.0));

        var sign = inverse ? -T.One : T.One;

        var dx = sign * offset.XYZOffset.X;
        var dy = sign * offset.XYZOffset.Y;
        var dz = sign * offset.XYZOffset.Z;
        var rx = sign * offset.RX * arcSecToRad;
        var ry = sign * offset.RY * arcSecToRad;
        var rz = sign * offset.RZ * arcSecToRad;
        var s = T.One + sign * offset.SC * T.CreateChecked(1e-6);

        // Coordinate-frame (Burša–Wolf / IERS) rotation matrix (small-angle linearization):
        //   R = [  1,  rz, -ry ]
        //       [ -rz,  1,  rx ]
        //       [  ry, -rx,  1 ]
        // A positive RZ maps the X-axis toward the positive Y-axis.
        var xNew = s * (x + rz * y - ry * z) + dx;
        var yNew = s * (-rz * x + y + rx * z) + dy;
        var zNew = s * (ry * x - rx * y + z) + dz;

        return (xNew, yNew, zNew);
    }
}