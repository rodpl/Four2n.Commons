﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtendedModel.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the EnumExtendedModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.NHibernate.Tests.Domain
{
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;

    public class EnumExtendedModel : BaseTestModel
    {
        public ExtendedTestEnum SampleEnum { get; set; }

        public ExtendedTestStringEnum StringEnum { get; set; }

        public ExtendedTestIntEnum IntEnum { get; set; }

        public ExtendedTestDecimalEnum DecimalEnum { get; set; }

        public ExtendedTestEnum? SampleNullableEnum { get; set; }
    }
}
