// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRangeModel.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateRangeModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.NHibernate.Tests.Domain
{
    using Four2n.Commons.System;

    public class DateRangeModel : BaseTestModel
    {
        public DateRange DatePeriod { get; set; }

        public DateTimeRange DateTimePeriod { get; set; }

        public DateRange? DatePeriodNullable { get; set; }

        public DateTimeRange? DateTimePeriodNullable { get; set; }
    }
}
