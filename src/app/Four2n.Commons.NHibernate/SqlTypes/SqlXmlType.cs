// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlXmlType.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the SqlXmlType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.NHibernate.SqlTypes
{
    using global::NHibernate.SqlTypes;

    using global::System;
    using global::System.Data;

    /// <summary>
    /// Native xml db type.
    /// </summary>
    [Serializable]
    public class SqlXmlType : SqlType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlXmlType"/> class.
        /// </summary>
        public SqlXmlType() : base(DbType.Xml)
        {
        }
    }
}