namespace NetFabric.Numerics.Rectangular2D.UnitTests;

public class SpanVectorAddTests
{
    /*
    
    public static TheoryData<Vector<double>[], Vector<double>, Vector<double>[]> AddValueData 
        => new()
        {
            {
                Array.Empty<Vector<double>>(), 
                new Vector<double>(1.0, 1.0), 
                Array.Empty<Vector<double>>()
            },
            {
                new Vector<double>[] { new(1.0, 2.0) }, 
                new Vector<double>(3.0, 4.0), 
                new Vector<double>[] { new(4.0, 6.0) }
            },
            {
                new Vector<double>[] { new(1.0, 2.0), new(11.0, 12.0) }, 
                new Vector<double>(3.0, 4.0), 
                new Vector<double>[] { new(4.0, 6.0), new(14.0, 16.0) }
            },
            { 
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value, value + 1)).ToArray(), 
                new Vector<double>(3.0, 4.0), 
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value + 3, value + 5)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Double_Value_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        // arrange
        var result = new Vector<double>[source.Length];

        // act
        Vector.Add(source, value, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Double_Value_Inplace_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        // arrange

        // act
        Vector.Add(source, value, source);

        // assert
        source.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Int_Value_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        // arrange
        var sourceInt = source.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();
        var valueInt = new Vector<int>(Convert.ToInt32(value.X), Convert.ToInt32(value.Y));
        var expectedInt = expected.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();
        var result = new Vector<int>[source.Length];

        // act
        Vector.Add(sourceInt, valueInt, result);

        // assert
        result.Should().Equal(expectedInt);
    }

    [Theory]
    [MemberData(nameof(AddValueData))]
    public void Add_Int_Value_Inplace_Should_Succeed(Vector<double>[] source, Vector<double> value, Vector<double>[] expected)
    {
        // arrange
        var sourceInt = source.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();
        var valueInt = new Vector<int>(Convert.ToInt32(value.X), Convert.ToInt32(value.Y));
        var expectedInt = expected.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();

        // act
        Vector.Add(sourceInt, valueInt, sourceInt);

        // assert
        sourceInt.Should().Equal(expectedInt);
    }

    public static TheoryData<Vector<double>[], Vector<double>[], Vector<double>[]> AddData 
        => new()
        {
            {
                Array.Empty<Vector<double>>(), 
                Array.Empty<Vector<double>>(), 
                Array.Empty<Vector<double>>()
            },
            {
                new Vector<double>[] { new(1.0, 2.0) }, 
                new Vector<double>[] { new(3.0, 4.0) },
                new Vector<double>[] { new(4.0, 6.0) }
            },
            {
                new Vector<double>[] { new(1.0, 2.0), new(11.0, 12.0) }, 
                new Vector<double>[] { new(3.0, 4.0), new(5.0, 6.0) },
                new Vector<double>[] { new(4.0, 6.0), new(16.0, 18.0) }
            },
            { 
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value, value + 1)).ToArray(), 
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value + 2, value + 3)).ToArray(),
                Enumerable.Range(0, 97).Select(value => new Vector<double>(value * 2 + 2, value * 2 + 4)).ToArray()
            },
        };

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Double_Should_Succeed(Vector<double>[] right, Vector<double>[] left, Vector<double>[] expected)
    {
        // arrange
        var result = new Vector<double>[right.Length];

        // act
        Vector.Add<double>(right, left, result);

        // assert
        result.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Double_Inplace_Should_Succeed(Vector<double>[] right, Vector<double>[] left, Vector<double>[] expected)
    {
        // arrange

        // act
        Vector.Add<double>(right, left, right);

        // assert
        right.Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Int_Should_Succeed(Vector<double>[] right, Vector<double>[] left, Vector<double>[] expected)
    {
        // arrange
        var rightInt = right.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();
        var leftInt = left.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();
        var expectedInt = expected.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();
        var result = new Vector<int>[right.Length];

        // act
        Vector.Add<int>(rightInt, leftInt, result);

        // assert
        result.Should().Equal(expectedInt);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Int_Inplace_Should_Succeed(Vector<double>[] right, Vector<double>[] left, Vector<double>[] expected)
    {
        // arrange
        var rightInt = right.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();
        var leftInt = left.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();
        var expectedInt = expected.Select(vector => new Vector<int>(Convert.ToInt32(vector.X), Convert.ToInt32(vector.Y))).ToArray();

        // act
        Vector.Add<int>(rightInt, leftInt, rightInt);

        // assert
        rightInt.Should().Equal(expectedInt);
    }

    */
}
