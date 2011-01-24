// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISimplifiedKeyIdentifiable.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the ISimplifiedKeyIdentifiable type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Collections
{
    /// <summary>
    /// Interface for an object which can be identified by SimplifiedKey
    /// </summary>
    /// <typeparam name="V">Value type for simlified key</typeparam>
    /// <typeparam name="K">Simplified key type</typeparam>
    public interface ISimplifiedKeyIdentifiable<V, K>
        where V : struct
        where K : SimplifiedKey<V>
    {
        /// <summary>
        /// Gets simplified key.
        /// </summary>
        K Key { get; }
    }
}