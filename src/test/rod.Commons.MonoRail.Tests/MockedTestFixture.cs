using NUnit.Framework;
using Rhino.Mocks;

namespace Rod.Commons.MonoRail
{
    /// <summary>
    /// Base class for test fixtures which uses mocks.
    /// </summary>
    public abstract class MockedTestFixture
    {
        protected MockRepository Mocks { get; set; }

        public virtual void SetUp()
        {
            Mocks = new MockRepository();
        }
    }
}