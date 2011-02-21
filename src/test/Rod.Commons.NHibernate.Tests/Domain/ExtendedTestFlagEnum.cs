// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedTestFlagEnum.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the ExtendedTestFlagEnum type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.Tests.Domain
{
    using System;

    using global::System;

    [Flags]
    public enum ExtendedTestFlagEnum
    {
        [EnumExtendedInfo(Name = "waiting", CustomValue = "...")]
        Pending = 1,
        [EnumExtendedInfo(Name = "misc")]
        Misc = 2,
        [EnumExtendedInfo(CustomValue = "dbName")]
        Something = 4,
        NonPending = 8,
        [EnumExtendedInfo(CustomValue = 12L)]
        Numeric = 16,
        [EnumExtendedInfo(CustomValue = 23)]
        Integer = 32
    }
}