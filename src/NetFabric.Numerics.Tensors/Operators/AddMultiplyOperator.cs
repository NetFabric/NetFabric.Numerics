namespace NetFabric.Numerics
{
    /// <summary>
    /// Represents an operator that performs addition and multiplication operations on three operands.
    /// </summary>
    /// <typeparam name="T">The type of the operands.</typeparam>
    public readonly struct AddMultiplyOperator<T> : ITernaryOperator<T>
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
    {
        /// <summary>
        /// Invokes the operator on three operands.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        /// <param name="z">The third operand.</param>
        /// <returns>The result of the addition and multiplication operations.</returns>
        public static T Invoke(T x, T y, T z)
            => (x + y) * z;

        /// <summary>
        /// Invokes the operator on three vector operands.
        /// </summary>
        /// <param name="x">The first vector operand.</param>
        /// <param name="y">The second vector operand.</param>
        /// <param name="z">The third vector operand.</param>
        /// <returns>The result of the addition and multiplication operations.</returns>
        public static Vector<T> Invoke(Vector<T> x, Vector<T> y, Vector<T> z)
            => (x + y) * z;
    }
}