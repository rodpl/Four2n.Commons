// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateTestCase.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the NHibernateTestCase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rod.Commons.NHibernate.Tests
{
    using global::NHibernate;
    using global::NHibernate.Engine;
    using global::NHibernate.Test;

    public abstract class NHibernateTestCase : TestCase
    {
        protected ISessionFactoryImplementor SessionFactoryImplementor { get { return this.Sfi; } }

        protected ISession Session { get; set; }

        protected ITransaction Trasaction { get; set; }

        protected override string MappingsAssembly
        {
            get { return "Rod.Commons.NHibernate.Tests"; }
        }

        protected override void OnSetUp()
        {
            this.Session = this.OpenSession();
            this.Trasaction = this.Session.BeginTransaction();
            this.SessionFactoryImplementor.Statistics.Clear();
        }

        protected override void OnTearDown()
        {

            this.Trasaction.Rollback();
//            this.Session.Flush();
            this.Session.Clear();
            this.Session.Close();
        }
    }
}
