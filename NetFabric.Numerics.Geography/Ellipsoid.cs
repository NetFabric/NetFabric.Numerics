﻿using System;

namespace NetFabric.Numerics.Geography;

/// <summary>
/// Summary description for Ellipsoid.
/// </summary>
public readonly record struct Ellipsoid(double EquatorialRadius, double Flattening)
{
    public static readonly Ellipsoid Airy1830 = new(6377563.396, 1.0 / 299.3249646);
    public static readonly Ellipsoid Australian1965 = new(6378160.0, 1.0 / 298.25);
    public static readonly Ellipsoid Bessel1841 = new(6377397.155, 1.0 / 299.1528128);
    public static readonly Ellipsoid Clarke1880 = new(6378249.145, 1.0 / 293.465);
    public static readonly Ellipsoid Clarke1866 = new(6378206.4, 1.0 / 294.9786982);
    public static readonly Ellipsoid Everest1830 = new(6377276.345, 1.0 / 300.8017);
    public static readonly Ellipsoid Fischer1960 = new(6378166.0, 1.0 / 298.3);
    public static readonly Ellipsoid Fischer1968 = new(6378150.0, 1.0 / 298.3);
    public static readonly Ellipsoid Grs1967 = new(6378160.0, 1.0 / 298.247167427);
    public static readonly Ellipsoid Grs1975 = new(6378140.0, 1.0 / 298.257);
    public static readonly Ellipsoid Grs1980 = new(6378137.0, 1.0 / 298.257222101);
    public static readonly Ellipsoid Hayford1924 = new(6378388.0, 1.0 / 297.0);
    public static readonly Ellipsoid Helmert1906 = new(6378200.0, 1.0 / 298.3);
    public static readonly Ellipsoid Hough1956 = new(6378270.0, 1.0 / 297.0);
    public static readonly Ellipsoid International1924 = new(6378388.0, 1.0 / 297.0);
    public static readonly Ellipsoid Krassovsky1940 = new(6378245.0, 1.0 / 298.3);
    public static readonly Ellipsoid SouthAmerican1969 = new(6378160.0, 1.0 / 298.25);
    public static readonly Ellipsoid Wgs1960 = new(6378165.0, 1.0 / 298.3);
    public static readonly Ellipsoid WGS1966 = new(6378145.0, 1.0 / 298.25);
    public static readonly Ellipsoid WGS1972 = new(6378135.0, 1.0 / 298.26);
    public static readonly Ellipsoid WGS1984 = new(6378137.0, 1.0 / 298.257223563);

    /// <summary>
    /// Gets the surface area of the ellipsoid.
    /// </summary>
    /// <value>The surface area of the ellipsoid.</value>
    public double SurfaceArea
    {
        get
        {
            var e = this.Eccentricity;
            return (2 * Math.PI * Math.Pow(EquatorialRadius, 2.0)) + (Math.PI * (Math.Pow(this.PolarRadius, 2.0) / e) * Math.Log10((1 + e) / (1 - e)));
        }
    }

    /// <summary>
    /// Gets the volume of the ellipsoid.
    /// </summary>
    /// <value>The volume of the ellipsoid.</value>
    public double Volume
        => 4 * Math.PI * Math.Pow(EquatorialRadius, 2.0) * this.PolarRadius / 3.0;

    /// <summary>
    /// Gets the radius of curvatures at the poles.
    /// </summary>
    /// <value>The radius of curvatures at the poles.</value>
    public double RadiusOfCurvatureAtPoles
        => EquatorialRadius / Math.Sqrt(1 - this.EccentricitySquared);

    /// <summary>
    /// Gets the radius of curvature in a meridian plane at the equator.
    /// </summary>
    /// <value>The radius of curvature in a meridian plane at the equator.</value>
    public double RadiusOfCurvatureAtEquator
        => EquatorialRadius * (1 - this.EccentricitySquared);

    public double PolarRadius
        => EquatorialRadius * (1 - Flattening);

    public double Eccentricity
        => Math.Sqrt(EccentricitySquared);

    public double EccentricitySquared
        => Flattening * (2 - Flattening);
}
