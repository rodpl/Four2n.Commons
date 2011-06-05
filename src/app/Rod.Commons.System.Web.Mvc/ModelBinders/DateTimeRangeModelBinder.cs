// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRangeModelBinder.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Binder for <see cref="DateTimeRange" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System.Web.Mvc.ModelBinders
{
    using global::System;

    /// <summary>
    /// Binder for <see cref="DateTimeRange"/>
    /// </summary>
    public class DateTimeRangeModelBinder : DateTimeRangeModelBinder<DateTimeRange>
    {
        /// <summary>
        /// Creates DateRange Implementation
        /// </summary>
        /// <param name="begins">Range begins.</param>
        /// <param name="ends">Range ends.</param>
        /// <returns>Instance of DataTimeRange struct</returns>
        protected override DateTimeRange Create(DateTime? begins, DateTime? ends)
        {
            return new DateTimeRange(begins, ends);
        }
    }
}