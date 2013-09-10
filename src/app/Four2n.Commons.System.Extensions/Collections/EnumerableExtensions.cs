// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumerableExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Extensions.Collections
{
    using System.Collections;

    using global::System.Collections;
    using global::System.Collections.Generic;

    /// <summary>
    /// Extension methods for Enumerable.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Joins one enumerator with another as yield
        /// </summary>
        /// <param name="first">First enumerator.</param>
        /// <param name="second">Second enumerator.</param>
        /// <returns>Joined enumerators as yield.</returns>
        public static IEnumerable YieldWith(this IEnumerable first, IEnumerable second)
        {
            return CollectionTools.JoinEnumerators(first, second);
        }

        /// <summary>
        /// Joins one enumerator with another as yield
        /// </summary>
        /// <typeparam name="T">Type of enumerated.</typeparam>
        /// <param name="first">First enumerator.</param>
        /// <param name="second">Second enumerator.</param>
        /// <returns>
        /// Joined enumerators as yield.
        /// </returns>
        public static IEnumerable<T> YieldWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return CollectionTools.JoinEnumerators(first, second);
        }
    }
}
