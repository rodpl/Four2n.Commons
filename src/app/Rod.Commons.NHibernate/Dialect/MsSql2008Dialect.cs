// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MsSql2008Dialect.cs" company="Daniel Dabrowski - rod.blogsome.com">
//   Copyright (c) Daniel Dabrowski - rod.blogsome.com.  All rights reserved.
// </copyright>
// <summary>
//   Defines the MsSql2008Dialect type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.Dialect
{
    using System.Data;

    /// <summary>
    /// Customized dialect for MS Sql 2008.
    /// </summary>
    public class MsSql2008Dialect : global::NHibernate.Dialect.MsSql2008Dialect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsSql2008Dialect"/> class.
        /// </summary>
        public MsSql2008Dialect()
        {
            RegisterColumnType(DbType.Xml, "XML");
        }
    }
}
