// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlXmlStringType.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the SqlXmlStringType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.NHibernate.SqlTypes
{
    using global::NHibernate.SqlTypes;

    using global::System;
    using global::System.Data;

    /// <summary>
    /// Xml type as string 4000 chars long.
    /// </summary>
    [Serializable]
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