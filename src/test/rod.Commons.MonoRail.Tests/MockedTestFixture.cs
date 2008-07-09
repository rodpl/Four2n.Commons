using MbUnit.Framework;
using Rhino.Mocks;

namespace rod.Commons.MonoRail.Tests
{
    /// <summary>
    /// Base class for test fixtures which uses mocks.
    /// </summary>
    public abstract class MockedTestFixture
    {
        protected MockRepository Mocks { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            Mocks = new MockRepository();
        }
    }
}