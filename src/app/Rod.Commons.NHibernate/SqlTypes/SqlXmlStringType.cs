// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlXmlStringType.cs" company="Daniel Dabrowski - rod.blogsome.com">
//   Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>
//   Defines the SqlXmlStringType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.SqlTypes
{
    using System.Data;

    using global::NHibernate.SqlTypes;

    /// <summary>
    /// Xml type as string 4000 chars long.
    /// </summary>
    public class SqlXmlStringType : SqlType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlXmlStringType"/> class.
        /// </summary>
        public SqlXmlStringType() : base(DbType.String, 4000)
        {
        }
    }
}