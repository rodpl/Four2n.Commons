// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlagEnumExtendedModel.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the FlagEnumExtendedModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.NHibernate.Tests.Domain
{
    public class FlagEnumExtendedModel : BaseTestModel
    {
        public ExtendedTestFlagEnum SampleEnum { get; set; }

        public ExtendedTestFlagEnum SampleEnumTwo { get; set; }
    }
}