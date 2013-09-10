// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelperExtensions.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the ReflectionHelperExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Extensions.Reflection
{
    using System.Reflection;

    /// <summary>
    /// Extensions for <see cref="ReflectionHelper"/>.
    /// </summary>
    public static class ReflectionHelperExtensions
    {
        /// <summary>
        /// Reflects the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns><see cref="ReflectionHelper"/> instance</returns>
        public static ReflectionHelper Reflect(this object target)
        {
            return ReflectionHelper.For(target);
        }
    }
}