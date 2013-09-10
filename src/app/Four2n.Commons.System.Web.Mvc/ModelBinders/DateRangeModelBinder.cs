// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRangeModelBinder.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateRangeModelBinder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Web.Mvc.ModelBinders
{
    using global::System;

    /// <summary>
    /// Binder for <see cref="DateRange"/>
    /// </summary>
    public class DateRangeModelBinder : DateTimeRangeModelBinder<DateRange>
    {
        /// <summary>
        /// Creates DateRange Implementation
        /// </summary>
        /// <param name="begins">Range begins.</param>
        /// <param name="ends">Range ends.</param>
        /// <returns>Instance of DataTimeRange struct</returns>
        protected override DateRange Create(DateTime? begins, DateTime? ends)
        {
            return new DateRange(begins, ends);
        }
    }
}