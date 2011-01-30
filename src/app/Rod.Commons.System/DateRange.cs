// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRange.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateRange type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.System
{
    using global::System;

    /// <summary>
    /// Interface for DateRange structures.
    /// </summary>
    public interface IDateTimeRange
    {
        /// <summary>
        /// Gets beggining date of range.
        /// Returns null if there is no beggining date
        /// </summary>
        DateTime? Begins { get; }

        /// <summary>
        /// Gets end date of range.
        /// Returns null if there is no end date.
        /// </summary>
        DateTime? Ends { get; }
    }

    /// <summary>
    /// Structure of date range as period of time with a day as minimal unit of time.
    /// </summary>
    public struct DateRange : IDateTimeRange, IEquatable<DateRange>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> struct. 
        /// </summary>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate"> The end date. </param>
        public DateRange(DateTime? startDate, DateTime? endDate) : this()
        {
            this.Begins = startDate.HasValue ? startDate.Value.Date : startDate;
            if (endDate == DateTime.MaxValue)
            {
                this.Ends = endDate;
            }
            else
            {
                this.Ends = endDate.HasValue ? endDate.Value.Date.AddDays(1).AddMilliseconds(-1) : endDate;
            }

            if (startDate.HasValue && endDate.HasValue && this.Begins > this.Ends)
            {
                throw new ArgumentOutOfRangeException("startDate", string.Format("Start date {0} is from day which is greater than end date {1}.", startDate, endDate));
            }
        }

        /// <summary>
        /// Gets beggining date of range.
        /// Returns null if there is no beggining date
        /// </summary>
        public DateTime? Begins { get; private set; }

        /// <summary>
        /// Gets end date of range.
        /// Returns null if there is no end date.
        /// </summary>
        public DateTime? Ends { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="x">The first <see cref="object"/> to compare with.</param>
        /// <param name="y">The second <see cref="object"/> to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public static new bool Equals(object x, object y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null && y != null)
            {
                return false;
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj.GetType() != typeof(DateRange))
            {
                return false;
            }

            return this.Equals((DateRange)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(DateRange other)
        {
            return this.Begins == other.Begins && this.Ends == other.Ends;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Begins.HasValue ? this.Begins.Value.GetHashCode() : 0) * 397) ^ (this.Ends.HasValue ? this.Ends.Value.GetHashCode() : 0);
            }
        }
    }

    /// <summary>
    /// Structure of date range as period of time with a tick as minimal unit of time.
    /// </summary>
    public struct DateTimeRange : IDateTimeRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> struct. 
        /// </summary>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate">The end date.</param>
        public DateTimeRange(DateTime? startDate, DateTime? endDate) : this()
        {
            if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
            {
                throw new ArgumentOutOfRangeException("startDate", string.Format("Start date {0} is greater than end date {1}.", startDate, endDate));
            }

            this.Ends = endDate;
            this.Begins = startDate;
        }

        /// <summary>
        /// Gets beggining date of range.
        /// Returns null if there is no beggining date
        /// </summary>
        public DateTime? Begins { get; private set; }

        /// <summary>
        /// Gets end date of range.
        /// Returns null if there is no end date.
        /// </summary>
        public DateTime? Ends { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="x">The first <see cref="object"/> to compare with.</param>
        /// <param name="y">The second <see cref="object"/> to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public static new bool Equals(object x, object y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null && y != null)
            {
                return false;
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj.GetType() != typeof(DateTimeRange))
            {
                return false;
            }

            return this.Equals((DateTimeRange)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(DateTimeRange other)
        {
            return this.Begins == other.Begins && this.Ends == other.Ends;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Begins.HasValue ? this.Begins.Value.GetHashCode() : 0) * 397) ^ (this.Ends.HasValue ? this.Ends.Value.GetHashCode() : 0);
            }
        }
    }
}