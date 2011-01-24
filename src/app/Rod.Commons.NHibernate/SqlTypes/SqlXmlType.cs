// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlXmlType.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the SqlXmlType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.SqlTypes
{
    using System.Data;

    using global::NHibernate.SqlTypes;

    /// <summary>
    /// Native xml db type.
    /// </summary>
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