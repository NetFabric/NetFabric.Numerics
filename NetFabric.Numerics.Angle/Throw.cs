using System;

namespace NetFabric.Numerics
{
    static class Throw
    {
        public static void ArgumentException(string message, string paramName)
            => throw new ArgumentException(message, paramName);

        public static T ArgumentException<T>(string message, string paramName)
            => throw new ArgumentException(message, paramName);

        public static void ArgumentOutOfRangeException(string paramName, object actualValue, string message)
            => throw new ArgumentOutOfRangeException(paramName, actualValue, message);

        public static T InvalidOperationException<T>()
            => throw new InvalidOperationException();
    }
}