// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Runtime.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Util class for runtime.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System
{
    using global::System;

    /// <summary>
    /// Util class for runtime.
    /// </summary>
    public class Runtime
    {
        /// <summary>
        /// Is runtime x64.
        /// </summary>
        /// <returns>True if runtime is x64.</returns>
        public static bool Isx64()
        {
            return IntPtr.Size == 8;
        }
    }
}
