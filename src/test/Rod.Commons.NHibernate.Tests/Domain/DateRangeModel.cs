// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRangeModel.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateRangeModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.Tests.Domain
{
    using Rod.Commons.System;

    public class DateRangeModel
    {
        public int Id { get; set; }

        public DateRange DatePeriod { get; set; }

        public DateTimeRange DateTimePeriod { get; set; }

        public DateRange? DatePeriodNullable { get; set; }

        public DateTimeRange? DateTimePeriodNullable { get; set; }
    }
}
