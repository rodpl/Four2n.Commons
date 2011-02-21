// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedTestEnum.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the ExtendedTestEnum type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.Tests.Domain
{
    using System;

    public enum ExtendedTestEnum
    {
        [EnumExtendedInfo(Name = "waiting", CustomValue = "...")]
        Pending,
        [EnumExtendedInfo(Name = "misc")]
        Misc,
        [EnumExtendedInfo(CustomValue = "dbName")]
        Something,
        NonPending,
        [EnumExtendedInfo(CustomValue = 12L)]
        Numeric,
        [EnumExtendedInfo(CustomValue = 23)]
        Integer
    }
}