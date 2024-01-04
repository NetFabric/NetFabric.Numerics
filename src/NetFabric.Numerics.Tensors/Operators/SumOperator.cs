namespace NetFabric.Numerics
{
    /// <summary>
    /// Represents an operator that performs the sum aggregation operation on a tensor.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    public readonly struct SumOperator<T> : IAggregationOperator<T>
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        /// <summary>
        /// Gets the seed value for the sum operation.
        /// </summary>
        public static T Identity => T.AdditiveIdentity;

        /// <summary>
        /// Computes the result of the sum operation for a single value and a vector.
        /// </summary>
        /// <param name="value">The value to be added to the sum.</param>
        /// <param name="vector">The vector to be summed.</param>
        /// <returns>The result of the sum operation.</returns>
        public static T ResultSelector(T value, Vector<T> vector)
            => Vector.Sum(vector) + value;

        /// <summary>
        /// Computes the sum of two values.
        /// </summary>
        /// <param name="x">The first value to be added.</param>
        /// <param name="y">The second value to be added.</param>
        /// <returns>The sum of the two values.</returns>
        public static T Invoke(T x, T y)
            => x + y;

        /// <summary>
        /// Computes the sum of two vectors.
        /// </summary>
        /// <param name="x">The first vector to be added.</param>
        /// <param name="y">The second vector to be added.</param>
        /// <returns>The sum of the two vectors.</returns>
        public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
            => x + y;
    }

    /// <summary>
    /// Represents an operator that performs the sum aggregation operation on pairs of elements in a tensor.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    public readonly struct SumPairsOperator<T> : IAggregationPairsOperator<T>
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        /// <summary>
        /// Gets the seed value for the sum operation on pairs.
        /// </summary>
        public static ValueTuple<T, T> Identity => (T.AdditiveIdentity, T.AdditiveIdentity);

        /// <summary>
        /// Computes the result of the sum operation on pairs for a value and a vector.
        /// </summary>
        /// <param name="value">The value to be added to the sum.</param>
        /// <param name="vector">The vector to be summed.</param>
        /// <returns>The result of the sum operation on pairs.</returns>
        public static ValueTuple<T, T> ResultSelector(ValueTuple<T, T> value, Vector<T> vector)
        {
            for (var index = 0; index < Vector<T>.Count; index += 2)
            {
                value.Item1 += vector[index];
                value.Item2 += vector[index + 1];
            }
            return value;
        }

        /// <summary>
        /// Computes the sum of two values.
        /// </summary>
        /// <param name="x">The first value to be added.</param>
        /// <param name="y">The second value to be added.</param>
        /// <returns>The sum of the two values.</returns>
        public static T Invoke(T x, T y)
            => x + y;

        /// <summary>
        /// Computes the sum of two vectors.
        /// </summary>
        /// <param name="x">The first vector to be added.</param>
        /// <param name="y">The second vector to be added.</param>
        /// <returns>The sum of the two vectors.</returns>
        public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
            => x + y;
    }

    public readonly struct SumTripletsOperator<T> : IAggregationTripletsOperator<T>
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        public static ValueTuple<T, T, T> Seed => (T.AdditiveIdentity, T.AdditiveIdentity, T.AdditiveIdentity);

        public static ValueTuple<T, T, T> ResultSelector(ValueTuple<T, T, T> value, Vector<T> vector)
        {
            // for (var index = 0; index < Vector<T>.Count; index += 3)
            // {
            //     value.Item1 += vector[index];
            //     value.Item2 += vector[index + 1];
            // }
            return value;
        }

        public static T Invoke(T x, T y)
            => x + y;

        public static Vector<T> Invoke(Vector<T> x, Vector<T> y)
            => x + y;
    }
}