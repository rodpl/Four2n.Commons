// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MsSql2008Dialect.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the MsSql2008Dialect type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.NHibernate.Dialect
{
    using global::System.Data;

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
