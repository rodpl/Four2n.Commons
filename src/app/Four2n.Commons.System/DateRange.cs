// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRange.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateRange type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System
{
    using global::System;
    using global::System.ComponentModel;
    using global::System.Xml;
    using global::System.Xml.Schema;
    using global::System.Xml.Serialization;

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

        /// <summary>
        /// Returns true if date is later than begins date.
        /// </summary>
        /// <param name="date">DateTime to check.</param>
        /// <returns>True if date is later than begins date.</returns>
        bool LaterEqualThanBegins(DateTime? date);

        /// <summary>
        /// Returns true if date is earlier than ends date.
        /// </summary>
        /// <param name="date">DateTime to check.</param>
        /// <returns>True if date is earlier than ends date.</returns>
        bool EarlierEqualThanEnds(DateTime? date);

        /// <summary>
        /// Returns true if date is within defined range
        /// </summary>
        /// <param name="date">DateTime to check.</param>
        /// <returns>True if date is within defined range.</returns>
        bool Includes(DateTime date);
    }

    /// <summary>
    /// Structure of date range as period of time with a day as minimal unit of time.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(DateRangeTypeConverter))]
    public struct DateRange : IDateTimeRange, IEquatable<DateRange>, IXmlSerializable
    {
        /// <summary>
        /// Null object which means that period is infinite.
        /// </summary>
        public static DateRange Infinite = new DateRange(null, null);

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> struct. 
        /// </summary>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate"> The end date. </param>
        public DateRange(DateTime? startDate, DateTime? endDate) : this()
        {
            this.SetValues(startDate, endDate);
        }

        public DateRange(string text) : this()
        {
            var dates = ParseDateTimeRange(text);
            this.SetValues(dates[0], dates[1]);
        }

        internal static DateTime?[] ParseDateTimeRange(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new DateTime?[] { null, null };
            }

            var splits = text.Split(new[] { " - " }, StringSplitOptions.None);
            if (splits.Length != 2)
            {
                throw new ArgumentOutOfRangeException("text", text, "Cannot parse text");
            }

            var begins = string.IsNullOrEmpty(splits[0]) ? (DateTime?)null : DateTime.Parse(splits[0]);
            var ends = string.IsNullOrEmpty(splits[1]) ? (DateTime?)null : DateTime.Parse(splits[1]);
            return new DateTime?[] { begins, ends };
        }

        private void SetValues(DateTime? startDate, DateTime? endDate)
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
                throw new ArgumentOutOfRangeException(
                        "startDate",
                        string.Format("Start date {0} is from day which is greater than end date {1}.", startDate, endDate));
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

        public static implicit operator DateRange(string text)
        {
            return new DateRange(text);
        }

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

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Begins.HasValue ? this.Begins.Value.ToShortDateString() : "∞", this.Ends.HasValue ? this.Ends.Value.ToShortDateString() : "∞");
        }

        /// <summary>
        /// Returns true if date is later than begins date.
        /// </summary>
        /// <param name="date">DateTime to check.</param>
        /// <returns>True if date is later than begins date.</returns>
        public bool LaterEqualThanBegins(DateTime? date)
        {
            if (this.Begins == null)
            {
                return true;
            }

            if (date != null)
            {
                return date.Value >= this.Begins.Value;
            }

            return false;
        }

        /// <summary>
        /// Returns true if date is earlier than ends date.
        /// </summary>
        /// <param name="date">DateTime to check.</param>
        /// <returns>True if date is earlier than ends date.</returns>
        public bool EarlierEqualThanEnds(DateTime? date)
        {
            if (this.Ends == null)
            {
                return true;
            }

            if (date != null)
            {
                return date.Value <= this.Ends.Value;
            }

            return false;
        }

        /// <summary>
        /// Returns true if date is within defined range
        /// </summary>
        /// <param name="date">DateTime to check.</param>
        /// <returns>True if date is within defined range.</returns>
        public bool Includes(DateTime date)
        {
            return this.LaterEqualThanBegins(date) && this.EarlierEqualThanEnds(date);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var beginsString = reader.GetAttribute("Begins");
            var endsString = reader.GetAttribute("Ends");

            DateTime? begins = null;
            DateTime? ends = null;
            if (beginsString != null)
            {
                begins = DateTime.Parse(beginsString);
            }

            if (endsString != null)
            {
                ends = DateTime.Parse(endsString);
            }

            this.SetValues(begins, ends);
        }

        public void WriteXml(XmlWriter writer)
        {
            if (Begins.HasValue)
            {
                writer.WriteAttributeString("Begins", Begins.Value.ToShortDateString());
            }

            if (Ends.HasValue)
            {
                writer.WriteAttributeString("Ends", Ends.Value.ToShortDateString());
            }
        }
    }

    /// <summary>
    /// Structure of date range as period of time with a tick as minimal unit of time.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(DateTimeRangeTypeConverter))]
    public struct DateTimeRange : IDateTimeRange, IXmlSerializable
    {
        /// <summary>
        /// Null object which means that period is infinite.
        /// </summary>
        public static DateTimeRange Infinite = new DateTimeRange(null, null);

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> struct. 
        /// </summary>
        /// <param name="startDate"> The start date. </param>
        /// <param name="endDate">The end date.</param>
        public DateTimeRange(DateTime? startDate, DateTime? endDate) : this()
        {
            this.SetValues(startDate, endDate);
        }

        public DateTimeRange(string text) : this()
        {
            var dates = DateRange.ParseDateTimeRange(text);
            this.SetValues(dates[0], dates[1]);
        }

        private void SetValues(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
            {
                throw new ArgumentOutOfRangeException(
                        "startDate", string.Format("Start date {0} is greater than end date {1}.", startDate, endDate));
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

        public static implicit operator DateTimeRange(string text)
        {
            return new DateTimeRange(text);
        }

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

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Begins.HasValue ? this.Begins.Value.ToString() : "∞", this.Ends.HasValue ? this.Ends.Value.ToString() : "∞");
        }

        /// <summary>
        /// Returns true if date is later than begins date.
        /// </summary>
        /// <param name="date">DateTime to check.</param>
        /// <returns>True if date is later than begins date.</returns>
        public bool LaterEqualThanBegins(DateTime? date)
        {
            if (this.Begins == null)
            {
                return true;
            }

            if (date != null)
            {
                return date.Value >= this.Begins.Value;
            }

            return false;
        }

        /// <summary>
        /// Returns true if date is earlier than ends date.
        /// </summary>
        /// <param name="date">DateTime to check.</param>
        /// <returns>True if date is earlier than ends date.</returns>
        public bool EarlierEqualThanEnds(DateTime? date)
        {
            if (this.Ends == null)
            {
                return true;
            }

            if (date != null)
            {
                return date.Value <= this.Ends.Value;
            }

            return false;
        }

        /// <summary>
        /// Returns true if date is within defined range
        /// </summary>
        /// <param name="date">DateTime to check.</param>
        /// <returns>True if date is within defined range.</returns>
        public bool Includes(DateTime date)
        {
            return this.LaterEqualThanBegins(date) && this.EarlierEqualThanEnds(date);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var beginsString = reader.GetAttribute("Begins");
            var endsString = reader.GetAttribute("Ends");

            DateTime? begins = null;
            DateTime? ends = null;
            if (beginsString != null)
            {
                begins = DateTime.Parse(beginsString);
            }

            if (endsString != null)
            {
                ends = DateTime.Parse(endsString);
            }

            this.SetValues(begins, ends);
        }

        public void WriteXml(XmlWriter writer)
        {
            if (Begins.HasValue)
            {
                writer.WriteAttributeString("Begins", Begins.Value.ToShortDateString());
            }

            if (Ends.HasValue)
            {
                writer.WriteAttributeString("Ends", Ends.Value.ToShortDateString());
            }
        }
    }
}