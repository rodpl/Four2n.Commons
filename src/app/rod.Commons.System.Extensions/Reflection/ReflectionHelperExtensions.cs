//------------------------------------------------------------------------------------------------- 
// <copyright file="ReflectionHelperExtensions.cs" company="Daniel Dabrowski - rod.blogsome.com">
// Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>Defines the ReflectionHelperExtensions type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Rod.Commons.System.Extensions.Reflection
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